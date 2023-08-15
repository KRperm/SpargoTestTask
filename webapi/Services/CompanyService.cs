using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ICompanyService
    {
        PharmacyHelper PharmacyHelper { get; }
        ProductHelper ProductHelper { get; }
        ShipmentHelper ShipmentHelper { get; }
        WarehouseHelper WarehouseHelper { get; }
        IEnumerable<(Product product, double amount)> GetProductsByPharmacy(Guid pharmacyId);
    }

    public class CompanyService : ICompanyService
    {
        public PharmacyHelper PharmacyHelper { get; }
        public ProductHelper ProductHelper { get; }
        public ShipmentHelper ShipmentHelper { get; }
        public WarehouseHelper WarehouseHelper { get; }

        public CompanyService(DataContext context)
        {
            PharmacyHelper = new PharmacyHelper(context);
            ProductHelper = new ProductHelper(context);
            ShipmentHelper = new ShipmentHelper(context);
            WarehouseHelper = new WarehouseHelper(context);
        }

        public IEnumerable<(Product product, double amount)> GetProductsByPharmacy(Guid pharmacyId)
        {
            var warehouses = WarehouseHelper.GetAllByPharmacy(pharmacyId);
            var shipments = warehouses.SelectMany(x => ShipmentHelper.GetAllByWarehouse(x.Id));
            var productGroups = shipments.GroupBy(x => x.Product);
            var stocks = productGroups.Select(g => (g.Key, g.Sum(x => x.Amount)));
            return stocks;
        }
    }
}
