using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Actions;
using WebApplication.Models;

namespace WebApplication
{
    public class MonitorService
    {
        WallsMonitorDbContext dbContext;
        public MonitorService(WallsMonitorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddMetrics(Messages.MetricsModel metricsModel)
        {
            var action = new AddMetricsAction(dbContext, metricsModel.MachineName, metricsModel.TimeStamp, metricsModel.Data);
            await action.Do();
        }
        public async Task AddJobs(List<Messages.JobModel> jobs)
        {
            var action = new AddJobsAction(dbContext, jobs);
            await action.Do();
        }
        public async Task AddLogs (Messages.LogModel logs)
        {
            var action = new AddLogsAction(dbContext, logs.NameOfLogSource, logs.Data);
            await action.Do();
        }
    }
}
