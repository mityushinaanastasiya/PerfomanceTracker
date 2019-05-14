using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class LogsController : ControllerBase
    {
        [HttpPost]
        public async Task Post([FromBody] Log model)
        {

        }
    }
}