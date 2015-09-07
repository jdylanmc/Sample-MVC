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
        void Create(Todo item);

        void Delete(int id);

        IEnumerable<Todo> GetTodoList();
        
        Todo GetItem(int id);
    }
}
