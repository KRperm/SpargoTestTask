using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class ShipmentHelper : GenericItemHelperBase<Shipment>
    {
        public ShipmentHelper(DataContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Shipment> GetAllByWarehouse(Guid warehouseId)
        {
            return DbSet
                .Where(x => x.WarehouseId == warehouseId)
                .Include(x => x.Product)
                .Include(x => x.Warehouse)
                .ToList();
        }

        protected override DbSet<Shipment> DbSet => context.Shipments;
    }
}
