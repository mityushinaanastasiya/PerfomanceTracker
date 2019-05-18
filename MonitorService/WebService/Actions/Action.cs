using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess;

namespace WebService.Actions
{
    public abstract class Action
    {
        protected WallsMonitorDbContext dbContext;
        public Action(WallsMonitorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
