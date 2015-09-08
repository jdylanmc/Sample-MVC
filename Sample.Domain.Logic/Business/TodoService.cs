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
using Sample.Infrastructure.Repository;

namespace Sample.Domain.Logic.Business
{
    public class TodoService : ITodoService
    {
        private EntityFrameworkContext context;

        public TodoService(EntityFrameworkContext entityFrameworkContext)
        {
            this.context = entityFrameworkContext;
        }

        public async Task CreateAsync(Todo item)
        {
            context.Todo.Add(item);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await context.Todo.FindAsync(id);

            if (item != null)
            {
                context.Todo.Remove(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Todo item)
        {
            context.Entry<Todo>(item).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public async Task<List<Todo>> GetTodoListAsync()
        {
            // return the first 20 for now...

            return await context.Todo.ToListAsync();
        }

        public async Task<Todo> GetItemAsync(int id)
        {
            var item = await context.Todo.FindAsync(id);

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
