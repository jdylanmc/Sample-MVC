using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Model.Exceptions
{
    [Serializable]
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message)
            : base(message) { }

        public ItemNotFoundException(int id, Type type)
            : base(String.Format("Object of type {0} was not found for id {1}", type.FullName, id)) { }

        public ItemNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public ItemNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
