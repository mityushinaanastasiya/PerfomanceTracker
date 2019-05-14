﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class LogsController : ControllerBase
    {
        MonitorService monitorService;
        public LogsController(MonitorService monitorService)
        {
            this.monitorService = monitorService;
        }
        [HttpPost]
        public async Task Post([FromBody] Messages.Log log)
        {
            await monitorService.AddLogs(log);
        }
    }
}