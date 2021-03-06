﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebService.Actions;
using WebService.DataAccess.Models;
using WebService.Models;
using WebService.ModelsForView;
using Type = WebService.Models.Type;

namespace WebService.Controllers
{
    public class HomeController : Controller
    {
        MonitorService monitorService;
        
        public HomeController(MonitorService monitorService)
        {
            this.monitorService = monitorService;
        }
        public IActionResult Index()
        {
            return View(monitorService.GetMachines());
        }

        public IActionResult LogsMain(string searchWord, DateTime startTime, DateTime endTime)
        {
            LogsMain logMain = new LogsMain(monitorService.GetCountsOfLogs(), monitorService.GetLogs());
            if (!String.IsNullOrEmpty(searchWord) && startTime > Convert.ToDateTime("01.01.0002")  && endTime > Convert.ToDateTime("01.01.0002"))
            {
                logMain = new LogsMain(monitorService.GetCountsOfLogs(), monitorService.GetLogs(searchWord, startTime, endTime));
            }
            if (!String.IsNullOrEmpty(searchWord) && startTime < Convert.ToDateTime("01.01.0002") && endTime < Convert.ToDateTime("01.01.0002"))
            {
                logMain = new LogsMain(monitorService.GetCountsOfLogs(), monitorService.GetLogs(searchWord));
            }
            if (String.IsNullOrEmpty(searchWord) && startTime > Convert.ToDateTime("01.01.0002") && endTime > Convert.ToDateTime("01.01.0002"))
            {
                logMain = new LogsMain(monitorService.GetCountsOfLogs(), monitorService.GetLogs(startTime, endTime));
            }

            return View(logMain);
        }

        public IActionResult MetricsMain(string machinaName, DateTime startTime, DateTime endTime)
        {
            var metrics = monitorService.GetMetricsMain();

            //if (metricsMain.StartDate > Convert.ToDateTime("01.01.0002") && metricsMain.EndDate > Convert.ToDateTime("01.01.0002"))
            //{
            //    metrics.Where(c => c.MachineName == metricsMain.MachineName).First().Memory = monitorService.GetMemory(metricsMain.MachineName, metricsMain.StartDate, metricsMain.EndDate);
            //}
            //foreach (var machine in metricsMain)
            //{
            //    if (machine.StartDate > Convert.ToDateTime("01.01.0002") && machine.EndDate > Convert.ToDateTime("01.01.0002"))
            //    {
            //        machine.Memory = monitorService.GetMemory(machine.MachineName, machine.StartDate, machine.EndDate);
            //        machine.Processor = monitorService.GetProcessor(machine.MachineName, machine.StartDate, machine.EndDate);
            //        machine.PhysicalDisk = monitorService.GetPhysicalDisk(machine.MachineName, machine.StartDate, machine.EndDate);
            //    }
            //}
            return View(metrics);
        }
        public IActionResult Logs(Type type)
        {
            return View(monitorService.GetLogs(type));
        }
        public IActionResult JobMain(string searchWord, DateTime startTime, DateTime endTime)
        {
            var JobMain = new JobMain(monitorService.GetJobs());
            if (!String.IsNullOrEmpty(searchWord) && startTime > Convert.ToDateTime("01.01.0002") && endTime > Convert.ToDateTime("01.01.0002"))
            {
                JobMain = new JobMain(monitorService.GetJobs(searchWord, startTime, endTime));
            }
            if (!String.IsNullOrEmpty(searchWord) && startTime < Convert.ToDateTime("01.01.0002") && endTime < Convert.ToDateTime("01.01.0002"))
            {
                JobMain = new JobMain(monitorService.GetJobs(searchWord));
            }
            if (String.IsNullOrEmpty(searchWord) && startTime > Convert.ToDateTime("01.01.0002") && endTime > Convert.ToDateTime("01.01.0002"))
            {
                JobMain = new JobMain(monitorService.GetJobs(startTime, endTime));
            }
            return View(JobMain);
        }

        public IActionResult Machine(string name)
        {
            return View(monitorService.GetMachine(name));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
