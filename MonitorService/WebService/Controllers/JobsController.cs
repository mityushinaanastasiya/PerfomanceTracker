using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api/jobs")]
    public class JobsController : Controller
    {
        MonitorService monitorService;
        public JobsController(MonitorService monitorService)
        {
            this.monitorService = monitorService;
        }

        [HttpPost]
        public async Task Post([FromBody] List<Messages.JobModel> jobs)
        {
            await monitorService.AddJobs(jobs);
        }
    }
}