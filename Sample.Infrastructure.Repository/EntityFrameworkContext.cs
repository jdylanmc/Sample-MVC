using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Sample.Domain.Model.Business;
using Sample.Domain.Model.System;
using Sample.Infrastructure.Data;

namespace Sample.Infrastructure.Repository
{
    /// <summary>
    /// There is no need to wrap EntityFramework up in some bogus IRepository, it already IS a repository!
    /// 
    /// Any really, how many times have you had to swap out an entire DAL layer?
    /// 
    /// Not to mention, the "context" object itself is your transaction manager... right?
    /// </summary>
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
