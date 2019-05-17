using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Actions
{
    public abstract class Action
    {
        protected WallsMonitorDbContext dbContext;
        public Action(WallsMonitorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public abstract Task Do();
    }
}
