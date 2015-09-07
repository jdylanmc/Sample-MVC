using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Interfaces
{
    public interface IRepository<T>
    {
        void Insert(T item);

        void Delete(T item);

        void Update(T item);

        IQueryable<T> Select();
    }
}
