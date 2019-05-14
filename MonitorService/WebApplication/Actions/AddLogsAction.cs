using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Actions
{
    public class AddLogsAction : Action
    {
        string nameOfLogSource;
        List<KeyValuePair<DateTime, string>> data;
        public AddLogsAction(WallsMonitorDbContext dbContext, string nameOfLogSource, List<KeyValuePair<DateTime, string>> data) : base(dbContext)
        {
            this.dbContext = dbContext;
            this.nameOfLogSource = nameOfLogSource;
            this.data = data;
        }

        public override async Task Do()
        {
            foreach (var obj in data)
            {
                string[] logPart = obj.Value.Split(new char[] { '|' });
                var log = new Log
                {
                    LogName = nameOfLogSource,
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
