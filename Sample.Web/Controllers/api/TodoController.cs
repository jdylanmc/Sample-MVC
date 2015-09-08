using System;
using System.Threading.Tasks;
using System.Web.Http;
using Sample.Domain.Interfaces.Business;
using Sample.Domain.Model.Business;
using Sample.Domain.Model.Exceptions;

namespace Sample.Web.Controllers.api
{
    public class TodoController : ApiController
    {
        private ITodoService todoService;

        public TodoController(ITodoService todoService)
        {
            this.todoService = todoService;
        }

        // GET api/todo
        public async Task<IHttpActionResult> Get()
        {
            var items = await todoService.GetTodoListAsync();

            return Ok(items);
        }

        // GET api/todo/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var item = await todoService.GetItemAsync(id);

                return Ok(item);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // POST api/todo
        public async Task<IHttpActionResult> Post(Todo item)
        {
            await todoService.CreateAsync(item);

            return Ok();
        }

        // PUT api/todo/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(Todo item)
        {
            await todoService.UpdateAsync(item);

            return Ok();
        }

        // DELETE api/todo/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await todoService.DeleteAsync(id);

            return Ok();
        }
    }
}
