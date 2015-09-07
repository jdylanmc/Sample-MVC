using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sample.Domain.Model.Business;

namespace Sample.Domain.Interfaces.Business
{
    public interface ITodoService
    {
        Task CreateAsync(Todo item);

        Task DeleteAsync(int id);

        Task<IEnumerable<Todo>> GetTodoListAsync();
        
        Task<Todo> GetItemAsync(int id);
    }
}
