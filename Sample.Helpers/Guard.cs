using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Helpers
{
    public static class Guard
    {
        public static void AgainstNullOrEmpty(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name", "Argument name cannot be null or empty.");
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(name, string.Format("Argument {0} cannot be null or empty.", name));
        }

        public static void AgainstNullOrWhitespace(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name", "Argument name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(name, string.Format("Argument {0} cannot be null or whitespace.", name));
        }

        public static void AgainstNull<T>(string name, T value)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name", "Argument name cannot be null or empty.");
            if (!default(T).Equals(null)) throw new InvalidOperationException("T must be a nullable type.");
            if (value == null) throw new ArgumentNullException(name, string.Format("Argument {0} cannot be null.", name));
        }

        public static void Against(bool condition, Action action)
        {
            if (condition) action();
        }
    }
}
