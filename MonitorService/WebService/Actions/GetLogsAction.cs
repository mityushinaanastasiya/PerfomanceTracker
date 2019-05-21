using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess;
using WebService.DataAccess.Models;
using WebService.Models;
using Type = WebService.Models.Type;

namespace WebService.Actions
{
    public class GetLogsAction : Action
    {
        public GetLogsAction(WallsMonitorDbContext dbContext) : base(dbContext)
        {
        }
        public List<Log> Get()
        {
            return dbContext.Logs.Take(20).ToList();
        }
        public  List<Log> Get(DateTime startTime, DateTime endTime)
        { 
            return dbContext.Logs.Where(d => d.TimeStamp >= startTime && d.TimeStamp <= endTime).ToList();
        }
        public List<Log> Get(string search)
        {
            return dbContext.Logs.Where(d => d.LogContent.Contains(search) || d.LogMessage.Contains(search) || d.LogSource.Contains(search) || d.MachineName.Contains(search) || d.MethodName.Contains(search) || d.ThreadId.Contains(search)).ToList();
        }
        public List<Log> Get(string search, DateTime startTime, DateTime endTime)
        {
            return dbContext.Logs.Where(d => d.TimeStamp >= startTime && 
            d.TimeStamp <= endTime &&
            (d.LogContent.Contains(search) || d.LogMessage.Contains(search) || d.LogSource.Contains(search) || d.MachineName.Contains(search) || d.MethodName.Contains(search) || d.ThreadId.Contains(search))).ToList();
        }
        public List<Log> Get (Type type)
        {
            return dbContext.Logs.Where(d => d.LogMessage == type.ToString()).ToList();
        }

        public Dictionary<string, int> GetCountsOfLogs()
        {
            Dictionary<string, int> countsOfLogs = new Dictionary<string, int>();
            countsOfLogs.Add(Type.Total.ToString(), this.Get(Type.Total).ToList().Count());
            countsOfLogs.Add(Type.Error.ToString(), this.Get(Type.Error).ToList().Count());
            countsOfLogs.Add(Type.Warning.ToString(), this.Get(Type.Warning).ToList().Count());
            countsOfLogs.Add(Type.Info.ToString(), this.Get(Type.Info).ToList().Count());
            countsOfLogs.Add(Type.Debug.ToString(), this.Get(Type.Debug).ToList().Count());
            return countsOfLogs;
        }
    }
}
