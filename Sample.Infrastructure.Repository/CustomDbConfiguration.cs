using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace Sample.Infrastructure.Data
{
    public class CustomDbConfiguration : DbConfiguration
    {
        public CustomDbConfiguration()
        {
            SetMigrationSqlGenerator(SqlProviderServices.ProviderInvariantName, () => new CustomSqlGenerator());
        }
    }
}