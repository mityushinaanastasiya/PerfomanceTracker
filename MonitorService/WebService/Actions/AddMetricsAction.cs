﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebService.DataAccess.Models;
using WebService.DataAccess;

namespace WebService.Actions
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
        public async Task Do()
        {
            var metrics = new Metrics
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
