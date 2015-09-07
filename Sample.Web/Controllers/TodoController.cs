using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index()
        {
            var todoList = todoService.GetTodoList();

            return View(todoList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                todoService.Create(todo);

                return RedirectToAction("Index");                
            }

            return View(todo);
        }

        public ActionResult Resolve(int id)
        {
            todoService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
