using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class MetricsController : Controller
    {
        MonitorService monitorService;
        public MetricsController (MonitorService monitorService)
        {
            this.monitorService = monitorService;
        }

        [HttpPost]
        public async Task Post([FromBody] Messages.MetricsModel model)
        {
            await monitorService.AddMetrics(model);
        }
    }
}