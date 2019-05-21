using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess.Models;

namespace WebService.ModelsForView
{
    public class Machine
    {
        public string Name { get; set; }
        public List<Log> Logs { get; set; }
        public List<DateTime> Dates { get; set; }
        public List<float> Processor { get; set; }
        public List<float> Memory { get; set; }
        public List<float> PhysicalDisk { get; set; }

        public Machine() { }

        public Machine(string name, List<Log> logs, List<DateTime> dates, List<float> processor, List<float> memory, List<float> physicalDisk)
        {
            Name = name;
            Logs = logs;
            Dates = dates;
            Processor = processor;
            Memory = memory;
            PhysicalDisk = physicalDisk;
        }
    }
}
