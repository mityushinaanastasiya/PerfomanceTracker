using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.ModelsForView
{
    public class MetricsMain
    {
        public string MachineName { get; set; }
        public List<DateTime> Dates { get; set; }
        public List<float> Processor { get; set; }
        public List<float> Memory { get; set; }
        public List<float> PhysicalDisk { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
