using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace Sample.Infrastructure.Data
{
    public class CustomSqlGenerator : SqlServerMigrationSqlGenerator
    {
        // if you want to change a key name, you need to override following Generate methods:

        protected override void Generate(AddForeignKeyOperation addForeignKeyOperation)
        {
            addForeignKeyOperation.Name = getFkName(addForeignKeyOperation.PrincipalTable, addForeignKeyOperation.DependentTable, addForeignKeyOperation.DependentColumns.ToArray());
            base.Generate(addForeignKeyOperation);
        }

        protected override void Generate(DropForeignKeyOperation dropForeignKeyOperation)
        {
            dropForeignKeyOperation.Name = getFkName(dropForeignKeyOperation.PrincipalTable, dropForeignKeyOperation.DependentTable, dropForeignKeyOperation.DependentColumns.ToArray());
            base.Generate(dropForeignKeyOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation)
        {
            createTableOperation.PrimaryKey.Name = getPkName(createTableOperation.Name);
            base.Generate(createTableOperation);
        }

        protected override void Generate(AddPrimaryKeyOperation addPrimaryKeyOperation)
        {
            addPrimaryKeyOperation.Name = getPkName(addPrimaryKeyOperation.Table);
            base.Generate(addPrimaryKeyOperation);
        }

        protected override void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation)
        {
            dropPrimaryKeyOperation.Name = getPkName(dropPrimaryKeyOperation.Table);
            base.Generate(dropPrimaryKeyOperation);
        }


        private string getPkName(string primaryKeyTable)
        {
            // here you can return any format you need.  this is useful when your DBAs have funny restrictions on naming conventions
            return ("PK_" + primaryKeyTable.Replace(".", "_"));
        }

        private string getFkName(string primaryKeyTable, string foreignKeyTable, params string[] foreignTableFields)
        {
            // here you can return any format you need.  this is useful when your DBAs have funny restrictions on naming conventions
            return ("FK_" + primaryKeyTable.Replace(".", "_") + "_" + foreignKeyTable.Replace(".", "_") + "_" + foreignTableFields[0]);
        }
    }
}