using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sample.Infrastructure.Interfaces;

namespace Sample.Infrastructure.Repository
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;

        private EntityFrameworkContext context;

        public EntityFrameworkRepository(EntityFrameworkContext entityFrameworkContext, IUnitOfWork unitOfWork)
        {
            var efUnitOfWork = unitOfWork as EntityFrameworkUnitOfWork;

            dbSet = efUnitOfWork.GetDbSet<T>();

            this.context = entityFrameworkContext;
        }

        public void Insert(T item)
        {
            dbSet.Add(item);
        }

        public T Update(T item, int id)
        {
            if (item == null)
                return null;

            T existing = dbSet.Find(id);
            
            if (existing != null)
            {
                context.Entry(existing).CurrentValues.SetValues(item);
            }
            return existing;
        }

        public void Delete(T item)
        {
            dbSet.Remove(item);
        }

        public IQueryable<T> Select()
        {
            return dbSet;
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().SingleOrDefault(match);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().Where(match).ToListAsync();
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public Task<int> CountAsync()
        {
            return dbSet.CountAsync();
        }
    }
}