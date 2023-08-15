using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class PharmacyHelper : GenericItemHelperBase<Pharmacy>
    {
        public PharmacyHelper(DataContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Pharmacy> DbSet => context.Pharmacies;
    }
}
