using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Actions
{
    public class AddJobsAction : Action
    {
        List<Messages.JobModel> jobs;
        public AddJobsAction(WallsMonitorDbContext dbContext, List<Messages.JobModel> jobs) : base(dbContext)
        {
            this.dbContext = dbContext;
            this.jobs = jobs;
        }

        public override async Task Do()
        {
            foreach (var obj in jobs)
            {
                var job = new Job
                {
                    ExtensionServiceJobsId = obj.ExtensionServiceJobsId,
                    ExtensionServiceName = obj.ExtensionServiceName,
                    ExtensionType = obj.ExtensionType,
                    LibraryName = obj.LibraryName,
                    JobType = obj.JobType,
                    JobState = obj.JobState,
                    FinalStatus = obj.FinalStatus,
                    Retries = obj.Retries,
                    QueueTime = obj.QueueTime,
                    StateLastChangedTime = obj.StateLastChangedTime,
                    StartTime = obj.StartTime,
                    EndTime = obj.EndTime,
                    Messages = obj.Messages,
                    OperationId = obj.OperationId
                };
                dbContext.Add(job);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
