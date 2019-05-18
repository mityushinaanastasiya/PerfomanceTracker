using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Actions;
using WebService.DataAccess;
using WebService.DataAccess.Models;

namespace WebService
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
        public List<Log> GetLogs(DateTime startTime, DateTime endTime)
        {
            var action = new GetLogsAction(dbContext);
            return action.Get(startTime, endTime);
        }
        public List<Log> GetLogs(string logType)
        {
            var action = new GetLogsAction(dbContext);
            return action.Get(logType);
        }
        public List<Log> GetLogs()
        {
            //var action = new GetLogsAction(dbContext);
            //return action.Get();
            List<Log> lg = new List<Log>();
            for (int i = 0; i<10; i++)
            {
                lg.Add(new Log { ClassName = "Class Name", LogContent = "Log Content", LogMessage = "Log Message", LogName = "Log Name", MachineName = "Machine Name", MethodName = "Method Name", ThreadId = "ThreadId", TimeStamp = DateTime.Now });
            }
            return lg;
        }
    }
}


