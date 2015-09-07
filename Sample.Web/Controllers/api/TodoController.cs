using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Sample.Domain.Interfaces.Business;
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
        public IHttpActionResult Get()
        {
            var items = todoService.GetTodoListAsync();

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
    }
}
