using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebService.DataAccess.Models;
using WebService.DataAccess;

namespace WebService.Actions
{
    public class AddLogsAction : Action
    {
        string logSource, machineName;
        List<KeyValuePair<DateTime, string>> data;
        public AddLogsAction(WallsMonitorDbContext dbContext, string machineName, string logSource, List<KeyValuePair<DateTime, string>> data) : base(dbContext)
        {
            this.machineName = machineName;
            this.logSource = logSource;
            this.data = data;
        }

        public async Task Do()
        {
            foreach (var obj in data)
            {
                string[] logPart = obj.Value.Split(new char[] { '|' });
                var log = new Log
                {
                    LogSource = logSource,
                    MachineName = machineName,
                    TimeStamp = obj.Key,
                    ThreadId = logPart[1].Trim(),
                    LogMessage = logPart[2].Trim(),
                    ClassName = logPart[3].Trim(),
                    MethodName = logPart[4].Trim(),
                    LogContent = logPart[5].Trim(),
                };
                dbContext.Add(log);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
