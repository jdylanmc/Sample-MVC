using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Insert(T item);

        T Update(T item, int id);

        void Delete(T item);

        IQueryable<T> Select();

        T Get(int id);

        Task<T> GetAsync(int id);

        T Find(Expression<Func<T, bool>> match);

        Task<T> FindAsync(Expression<Func<T, bool>> match);

        ICollection<T> FindAll(Expression<Func<T, bool>> match);

        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);

        int Count();
        
        Task<int> CountAsync();
    }
}
