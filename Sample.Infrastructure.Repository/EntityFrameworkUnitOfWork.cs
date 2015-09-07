using System.Data.Entity;
using System.Threading.Tasks;
using Sample.Infrastructure.Interfaces;

namespace Sample.Infrastructure.Repository
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly DbContext entityFrameworkContext;

        public EntityFrameworkUnitOfWork(EntityFrameworkContext entityFrameworkContext)
        {
            this.entityFrameworkContext = entityFrameworkContext;
        }

        internal DbSet<T> GetDbSet<T>() where T : class
        {
            return entityFrameworkContext.Set<T>();
        }
        
        public void Commit()
        {
            entityFrameworkContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await entityFrameworkContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            // you may choose to not save here, and instead rollback
            // I prefer rolling back...
            // entityFrameworkContext.Dispose();
        }
    }
}