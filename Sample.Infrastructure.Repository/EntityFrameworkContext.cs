using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Sample.Domain.Model.Business;
using Sample.Domain.Model.System;
using Sample.Infrastructure.Data;

namespace Sample.Infrastructure.Repository
{
    [DbConfigurationType(typeof(CustomDbConfiguration))]
    public class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext() : base("EntityFrameworkContext")
        {
            // uncomment the below line if you want to manually manage the database 
            // this is useful if you want to introduce a parallel tech like Dapper
            // Database.SetInitializer<EntityFrameworkContext>(null);

            // auto generate database for demonstration purposes only
            // if you want to keep using EF, you will probably introduce a db migration strategy
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityFrameworkContext>());
        }

        public DbSet<HttpAudit> HttpAudit { get; set; }

        public DbSet<Log> Log { get; set; }

        public DbSet<Todo> Todo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            // set the maximum length of any non-explicit data annotations to 256.
            // this is useful if you work with DBAs who hate nvarchar(max)
            modelBuilder.Properties<string>().Configure(x => x.HasMaxLength(256).IsUnicode(true));
        }
    }
}
