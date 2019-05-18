using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.DataAccess.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string LogName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ThreadId { get; set; }
        public string LogMessage { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string LogContent { get; set; }
        public string MachineName { get; set; }
    }
}
