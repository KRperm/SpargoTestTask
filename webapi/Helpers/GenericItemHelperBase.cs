using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Helpers
{
    public abstract class GenericItemHelperBase<TEntity> where TEntity : class, IIdentifiable
    {
        protected readonly DataContext context;

        protected GenericItemHelperBase(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public void Create(TEntity item)
        {
            DbSet.Add(item);
            context.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var item = DbSet.SingleOrDefault(x => x.Id == id);
            if (item is null)
                return false;
            DbSet.Remove(item);
            context.SaveChanges();
            return true;
        }

        public bool ItemExists(Guid id)
        {
            return DbSet.Any(x => x.Id == id);
        }

        protected abstract DbSet<TEntity> DbSet { get; }
    }
}
