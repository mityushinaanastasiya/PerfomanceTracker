using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess;
using WebService.DataAccess.Models;

namespace WebService.Actions
{
    public class GetLogsAction : Action
    {
        public enum LogType { Total, Error, Warning, Info, Debug }
        public GetLogsAction(WallsMonitorDbContext dbContext) : base(dbContext)
        {
        }
        public List<Log> Get()
        {
            return dbContext.Logs.Take(10).ToList();
        }
        public  List<Log> Get(DateTime startTime, DateTime endTime)
        { 
            return dbContext.Logs.Where(d => d.TimeStamp >= startTime && d.TimeStamp <= endTime).ToList();
        }
        public List<Log> Get (string logType)
        {
            return dbContext.Logs.Where(d => d.LogMessage == logType.ToString()).ToList();
        }
    }
}
