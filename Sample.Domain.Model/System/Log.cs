using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Model.System
{
    /// <summary>
    /// Useful for pulling log messages out of the database
    /// </summary>
    public class Log
    {
        public Log() { }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Thread { get; set; }

        public string Level { get; set; }

        public string Logger { get; set; }

        [MaxLength(4000)]
        public string Message { get; set; }
        
        [MaxLength(4000)]
        public string Exception { get; set; }
    }
}
