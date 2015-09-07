using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domain.Interfaces.Business;
using Sample.Domain.Model.Business;
using Sample.Domain.Model.Exceptions;
using Sample.Infrastructure.Interfaces;

namespace Sample.Domain.Logic.Business
{
    public class TodoService : ITodoService
    {
        private IRepository<Todo> repository;
        private IUnitOfWork unitOfWork;

        public TodoService(IRepository<Todo> repo, IUnitOfWork unitOfWork)
        {
            this.repository = repo;
            this.unitOfWork = unitOfWork;
        }

        public void Create(Todo item)
        {
            repository.Insert(item);
            unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var item = repository.Select().Where(x => x.Id == id).FirstOrDefault();

            if (item != null)
            {
                repository.Delete(item);
                unitOfWork.Commit();
            }
        }

        public IEnumerable<Todo> GetTodoList()
        {
            // return the first 20 for now...

            return repository.Select().Take(20);
        }

        public Todo GetItem(int id)
        {
            var item = repository.Select().FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                return item;
            }
            else
            {
                throw new ItemNotFoundException(id, typeof(Todo));
            }
        }
    }
}
