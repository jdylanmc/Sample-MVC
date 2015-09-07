using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task CreateAsync(Todo item)
        {
            repository.Insert(item);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await repository.GetAsync(id);

            if (item != null)
            {
                repository.Delete(item);
                await unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Todo>> GetTodoListAsync()
        {
            // return the first 20 for now...

            return await repository.Select().Take(20).ToListAsync();
        }

        public async Task<Todo> GetItemAsync(int id)
        {
            var item = await repository.GetAsync(id);

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
