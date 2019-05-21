using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Actions;
using WebService.DataAccess;
using WebService.DataAccess.Models;
using WebService.Models;
using WebService.ModelsForView;
using Type = WebService.Models.Type;

namespace WebService
{
    public class MonitorService
    {

        WallsMonitorDbContext dbContext;
        public MonitorService(WallsMonitorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddMetrics(Messages.MetricsModel metricsModel)
        {
            var action = new AddMetricsAction(dbContext, metricsModel.MachineName, metricsModel.TimeStamp, metricsModel.Data);
            await action.Do();
        }
        public async Task AddLogs(Messages.LogModel logs)
        {
            var action = new AddLogsAction(dbContext, logs.MachineName, logs.LogSource, logs.Data);
            await action.Do();
        }
        public async Task AddJobs(List<Messages.JobModel> jobs)
        {
            var action = new AddJobsAction(dbContext, jobs);
            await action.Do();
        }
        public List<Log> GetLogs(String searchWord, DateTime startTime, DateTime endTime)
        {
            var action = new GetLogsAction(dbContext);
            return action.Get(searchWord, startTime, endTime);
        }
        public List<Log> GetLogs(DateTime startTime, DateTime endTime)
        {
            var action = new GetLogsAction(dbContext);
            return action.Get(startTime, endTime);
        }
        public List<Log> GetLogs(String searchWord)
        {
            var action = new GetLogsAction(dbContext);
            return action.Get(searchWord);
        }
        public List<Log> GetLogs(Type type)
        {
            var action = new GetLogsAction(dbContext);
            return action.Get(type);
        }
        public List<Log> GetLogs()
        {
            var action = new GetLogsAction(dbContext);
            return action.Get();
        }
        public Machine GetMachine(string nameOfMachine)
        {
            Machine machine = new Machine();
            machine.Name = nameOfMachine;
            machine.Logs = this.GetLogs(nameOfMachine);
            machine.Processor = this.GetProcessor(nameOfMachine);
            machine.Memory = this.GetMemory(nameOfMachine);
            machine.PhysicalDisk = this.GetPhysicalDisk(nameOfMachine);
            machine.Dates = this.GetDates(nameOfMachine);
            return machine;
        }
        public List<MachineMain> GetMachines()
        {
            var action = new GetMachineAction(dbContext);
            return action.GetMachines();
        }
        public List<MetricsMain> GetMetricsMain()
        {
            List<MetricsMain> metrics = new List<MetricsMain>();
            var machines = this.GetMachines();
            foreach (var machine in machines)
            {
                MetricsMain oneMetrics = new MetricsMain();
                oneMetrics.MachineName = machine.Name;
                oneMetrics.Memory = this.GetMemory(oneMetrics.MachineName);
                oneMetrics.Processor = this.GetProcessor(oneMetrics.MachineName);
                oneMetrics.PhysicalDisk = this.GetPhysicalDisk(oneMetrics.MachineName);
                oneMetrics.Dates = this.GetDates(oneMetrics.MachineName);
                metrics.Add(oneMetrics);
            }
            return metrics;
        }
        public List<Job> GetJobs()
        {
            var action = new GetJobsAction(dbContext);
            return action.Get();
        }

        public List<Job> GetJobs(String searchWord, DateTime startTime, DateTime endTime)
        {
            var action = new GetJobsAction(dbContext);
            return action.Get(searchWord, startTime, endTime);
        }
        public List<Job> GetJobs(DateTime startTime, DateTime endTime)
        {
            var action = new GetJobsAction(dbContext);
            return action.Get(startTime, endTime);
        }
        public List<Job> GetJobs(String searchWord)
        {
            var action = new GetJobsAction(dbContext);
            return action.Get(searchWord);
        }
        public Dictionary<string, int> GetCountsOfLogs()
        {
            var action = new GetLogsAction(dbContext);
            return action.GetCountsOfLogs();
        }

        public List<float> GetProcessor(string machineName)
        {
            var action = new GetMetricsAction(dbContext);
            return action.GetProcessor(machineName);
        }
        public List<float> GetMemory(string machineName)
        {
            var action = new GetMetricsAction(dbContext);
            return action.GetMemory(machineName);
        }
        public List<float> GetPhysicalDisk(string machineName)
        {
            var action = new GetMetricsAction(dbContext);
            return action.GetPhysicalDisk(machineName);
        }
        public List<DateTime> GetDates(string machineName)
        {
            var action = new GetMetricsAction(dbContext);
            return action.GetDates(machineName);
        }
    }
}


