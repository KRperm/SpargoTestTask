using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class WarehouseHelper : GenericItemHelperBase<Warehouse>
    {
        public WarehouseHelper(DataContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Warehouse> GetAllByPharmacy(Guid pharmacyId)
        {
            return DbSet.Where(x => x.PharmacyId == pharmacyId).ToList();
        }

        protected override DbSet<Warehouse> DbSet => context.Warehouses;
    }
}
