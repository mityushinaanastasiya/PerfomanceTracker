using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ThreadId { get; set; }
        public string LogMessage { get; set; }
        public string LogContent { get; set; }
    }
}
