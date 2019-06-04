using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess;
using WebService.DataAccess.Models;

namespace WebService.Actions
{
    public class GetJobsAction : Action
    {
        public GetJobsAction(WallsMonitorDbContext dbContext) : base(dbContext)
        {
        }
        public List<Job> Get()
        {
            return dbContext.Jobs
                .Skip(Math.Max(0, dbContext.Jobs.Count() - 20))
                .OrderByDescending(o => o.QueueTime)
                .ToList();
        }
        public List<Job> Get(DateTime startTime, DateTime endTime)
        {
            return dbContext.Jobs.Where(d => d.QueueTime >= startTime && d.QueueTime <= endTime)
                .ToList();
        }
        public List<Job> Get(string search)
        {
            return dbContext.Jobs.Where(d => d.ExtensionServiceName.Contains(search) || d.ExtensionType.Contains(search) || d.LibraryName.Contains(search) || d.JobType.Contains(search) || d.JobState.Contains(search) || d.FinalStatus.Contains(search))
                .ToList();
        }
        public List<Job> Get(string search, DateTime startTime, DateTime endTime)
        {
            return dbContext.Jobs.Where(d => d.QueueTime >= startTime &&
            d.QueueTime <= endTime &&
            (d.ExtensionServiceName.Contains(search) || d.ExtensionType.Contains(search) || d.LibraryName.Contains(search) || d.JobType.Contains(search) || d.JobState.Contains(search) || d.FinalStatus.Contains(search)))
            .ToList();
        }

    }
}
