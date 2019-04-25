using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorService
{
    class MetriсsProvider
    {
        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        PerformanceCounter diskCounter;
        public MetriсsProvider()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }
        public Dictionary<string, string> GetMetrics() => new Dictionary<string, string>
                {
                    {"cpu", cpuCounter.NextValue().ToString()},
                    {"ram", ramCounter.NextValue().ToString()},
                    {"disk", diskCounter.NextValue().ToString()}
                };
    }
}
