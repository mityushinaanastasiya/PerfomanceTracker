using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class MetricsModel
    {
        public int Id { get; set; }
        public string MachineName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string PhysicalDisk { get; set; }
    }
}
