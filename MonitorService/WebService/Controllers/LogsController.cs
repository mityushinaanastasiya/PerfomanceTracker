using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        MonitorService monitorService;
        public LogsController(MonitorService monitorService)
        {
            this.monitorService = monitorService;
        }
        [HttpPost]
        public async Task Post([FromBody] Messages.LogModel log)
        {
            await monitorService.AddLogs(log);
        }
    }
}