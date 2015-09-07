using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domain.Model.Business;

namespace Sample.Infrastructure.Repository.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EntityFrameworkContext context)
        {
            context.Todo.Add(new Todo("Clean up the Create TODO Item page", "It's incredibly ugly right now."));
            context.Todo.Add(new Todo("Unit test IRepository"));
            context.Todo.Add(new Todo("Unit test IUnitOfWork"));
            context.Todo.Add(new Todo("Add more api methods", "We should support creating TODO items through the API"));
            context.Todo.Add(new Todo("Fix HttpAudit", "The HttpAuditor is broken, probably due to how it resolves dependencies"));
            context.Todo.Add(new Todo("Move logging to an ILog infrastructure item",
                "Log4Net is locking up our database like a beast.  Why?"));

            context.SaveChanges();
        }
    }
}
