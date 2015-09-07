using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Sample.Domain.Interfaces.Business;
using Sample.Domain.Model.Business;

namespace Sample.Web.Controllers
{
    public class TodoController : Controller
    {
        private ITodoService todoService;

        public TodoController(ITodoService todoService)
        {
            this.todoService = todoService;
        }

        public async Task<ActionResult> Index()
        {
            var todoList = await todoService.GetTodoListAsync();

            return View(todoList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                await todoService.CreateAsync(todo);

                return RedirectToAction("Index");                
            }

            return View(todo);
        }

        public async Task<ActionResult> Resolve(int id)
        {
            await todoService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
