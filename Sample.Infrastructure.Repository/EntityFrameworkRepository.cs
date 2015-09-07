using System;
using System.Data.Entity;
using System.Linq;
using Sample.Infrastructure.Interfaces;

namespace Sample.Infrastructure.Repository
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;

        public EntityFrameworkRepository(IUnitOfWork unitOfWork)
        {
            var efUnitOfWork = unitOfWork as EntityFrameworkUnitOfWork;

            dbSet = efUnitOfWork.GetDbSet<T>();
        }

        public void Insert(T item)
        {
            dbSet.Add(item);
        }

        public void Delete(T item)
        {
            dbSet.Remove(item);
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Select()
        {
            return dbSet;
        }
    }
}