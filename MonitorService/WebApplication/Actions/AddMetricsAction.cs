using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Actions
{
    public class AddMetricsAction : Action
    {
        Dictionary<string, string> data;
        string machineName;
        DateTime timeStamp;
        public AddMetricsAction (WallsMonitorDbContext dbContext, string MachineName, DateTime TimeStamp, Dictionary<string, string> Data) : base (dbContext)
        {
            this.machineName = MachineName;
            this.timeStamp = TimeStamp;
            this.data = Data;
        }
        public override async Task Do()
        {
            var metrics = new MetricsModel
            {
                MachineName = this.machineName,
                TimeStamp = this.timeStamp,
                PhysicalDisk = data["disk"],
                Processor = data["cpu"],
                Memory = data["ram"]
            };
            dbContext.Add(metrics);
            await dbContext.SaveChangesAsync();
        }

    }
}
