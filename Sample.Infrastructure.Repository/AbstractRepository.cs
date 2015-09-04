using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sample.Infrastructure.Data;

namespace Sample.Infrastructure.Repository
{
    public class AbstractRepository : IDisposable
    {
        protected EntityContext context;

        public AbstractRepository(EntityContext context)
        {
            this.context = context;
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // adapted from http://elegantcode.com/2012/01/26/sqlbulkcopy-for-generic-listt-useful-for-entity-framework-nhibernate/
        public void BulkInsert<T>(string tableName, IList<T> list)
        {
            using (var bulkCopy = new SqlBulkCopy(context.Database.Connection.ConnectionString.ToString()))
            {
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = tableName;

                var table = new DataTable();
                var props = TypeDescriptor.GetProperties(typeof(T))
                    // filter out the relationships/collections
                    .Cast<PropertyDescriptor>()
                    .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                    .ToArray();

                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }

                var values = new object[props.Length];
                foreach (var item in list)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }

                bulkCopy.WriteToServer(table);
            }
        }

        public string FormatDbEntityValidationException(DbEntityValidationException exception)
        {
            if (exception == null) return "";

            var sb = new StringBuilder();

            foreach (var result in exception.EntityValidationErrors)
            {
                sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    result.Entry.Entity.GetType().FullName, result.Entry.State));
                foreach (var error in result.ValidationErrors)
                {
                    sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                        error.PropertyName,
                        result.Entry.CurrentValues.GetValue<object>(error.PropertyName),
                        error.ErrorMessage));
                }
            }

            sb.AppendLine();

            return sb.ToString();
        }
    }
}
