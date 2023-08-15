using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class ProductHelper : GenericItemHelperBase<Product>
    {
        public ProductHelper(DataContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Product> DbSet => context.Products;
    }
}
