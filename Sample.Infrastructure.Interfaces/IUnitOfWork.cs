using System;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        Task CommitAsync();
    }
}