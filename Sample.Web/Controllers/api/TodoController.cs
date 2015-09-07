using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var items = todoService.GetTodoList();

            return Ok(items);
        }

        // GET api/todo/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var item = todoService.GetItem(id);

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
