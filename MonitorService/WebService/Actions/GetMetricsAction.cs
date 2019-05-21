using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess;

namespace WebService.Actions
{
    public class GetMetricsAction : Action
    {
        public GetMetricsAction(WallsMonitorDbContext dbContext) : base(dbContext)
        {
        }
        public List<float> GetProcessor (string machineName)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.MachineName == machineName)
                    .Select(d => float.Parse(d.Processor) )
                    .Take(10)
                    .ToList();
        }
        public List<float> GetMemory(string machineName)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.MachineName == machineName)
                    .Select(d => float.Parse(d.Memory))
                    .Take(10)
                    .ToList();
        }
        public List<float> GetPhysicalDisk(string machineName)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.MachineName == machineName)
                    .Select(d => float.Parse(d.PhysicalDisk))
                    .Take(10)
                    .ToList();

        }

        public List<DateTime> GetDates(string machineName)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.MachineName == machineName)
                    .Select(d => d.TimeStamp)
                    .Take(10)
                    .ToList();

        }
    }
}
