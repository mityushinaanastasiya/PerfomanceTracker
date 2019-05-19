using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess;
using WebService.DataAccess.Models;

namespace WebService.Actions
{
    public class GetJobsAction : Action
    {
        MonitorService monitorService;
        public GetJobsAction(WallsMonitorDbContext dbContext) : base(dbContext)
        {
        }
        public List<Job> Get()
        {
            return dbContext.Jobs.Take(20).ToList();
        }
    }
}
