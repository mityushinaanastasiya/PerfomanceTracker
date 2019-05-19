using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebService.Actions;
using WebService.DataAccess.Models;
using WebService.Models;

namespace WebService.Controllers
{
    public class HomeController : Controller
    {
        MonitorService monitorService;
        public enum LogType { Total, Error, Warning, Info, Debug }
        public HomeController(MonitorService monitorService)
        {
            this.monitorService = monitorService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logs()
        {
            List<Log> logsList = monitorService.GetLogs();
            return View(logsList);
        }

        public IActionResult Metrics()
        {
            return View();
        }

        public IActionResult Jobs()
        {
            List<Job> jobList = monitorService.GetJobs();
            return View(jobList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
