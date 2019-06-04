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
                    .Skip(Math.Max(0, dbContext.Metrics.Count() - 20))
                    .ToList();
        }
        public List<float> GetMemory(string machineName)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.MachineName == machineName)
                    .Select(d => float.Parse(d.Memory))
                    .Skip(Math.Max(0, dbContext.Metrics.Count() - 20))
                    .ToList();
        }
        public List<float> GetPhysicalDisk(string machineName)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.MachineName == machineName)
                    .Select(d => float.Parse(d.PhysicalDisk))
                    .Skip(Math.Max(0, dbContext.Metrics.Count() - 20))
                    .ToList();

        }
        public List<DateTime> GetDates(string machineName)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.MachineName == machineName)
                    .Select(d => d.TimeStamp)
                    .Skip(Math.Max(0, dbContext.Metrics.Count() - 20))
                    .ToList();

        }

        public List<float> GetProcessor(string machineName, DateTime startDate, DateTime endDate)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.TimeStamp>= startDate && s.TimeStamp <= endDate && s.MachineName == machineName)
                    .Select(d => float.Parse(d.Processor))
                    .Skip(Math.Max(0, dbContext.Metrics.Count() - 20))
                    .ToList();
        }
        public List<float> GetMemory(string machineName, DateTime startDate, DateTime endDate)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.TimeStamp >= startDate && s.TimeStamp <= endDate && s.MachineName == machineName)
                    .Select(d => float.Parse(d.Memory))
                    .Skip(Math.Max(0, dbContext.Metrics.Count() - 20))
                    .ToList();
        }
        public List<float> GetPhysicalDisk(string machineName, DateTime startDate, DateTime endDate)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.TimeStamp >= startDate && s.TimeStamp <= endDate && s.MachineName == machineName)
                    .Select(d => float.Parse(d.PhysicalDisk))
                    .Skip(Math.Max(0, dbContext.Metrics.Count() - 20))
                    .ToList();

        }
        public List<DateTime> GetDates(string machineName, DateTime startDate, DateTime endDate)
        {
            return dbContext.Metrics
                    .OrderBy(p => p.TimeStamp)
                    .Where(s => s.TimeStamp >= startDate && s.TimeStamp <= endDate && s.MachineName == machineName)
                    .Select(d => d.TimeStamp)
                    .Skip(Math.Max(0, dbContext.Metrics.Count() - 20))
                    .ToList();

        }
    }
}
