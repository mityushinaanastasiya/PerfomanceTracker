using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.ModelsForView
{
    public class MachineMain
    {
        public string Name { get; set; }
        public List<string> LogsSource { get; set; }
        public float LastCPU { get; set; }
        public float LastMemory { get; set; }
        public float LastPhysicalDisk { get; set; }
        public MachineMain()
        {
            this.LogsSource = new List<string>();
        }
        public MachineMain(string name, List<string> logsSource)
        {
            this.Name = name;
            this.LogsSource = new List<string>();
            this.LogsSource = logsSource;
        }
    }
}
