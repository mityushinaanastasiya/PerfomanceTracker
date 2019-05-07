using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Messages;
using System.IO;

namespace MonitorService
{
    class MonitorServiceProcess
    {
        enum MonitorServiceRole { Primary, Secondary}

        MetricServiceConfiguration metricServiceConfiguration;
        private List<Task> tasks;
        private MonitorServiceRole monitorServiceRole;
        public MonitorServiceProcess()
        {  
            metricServiceConfiguration = new MetricServiceConfiguration();
            MonitorServiceRole.TryParse(metricServiceConfiguration.MonitorServiseRole, true, out monitorServiceRole);
            tasks = new List<Task>();
        }

        public void MonitorProcess()
        {
            tasks.Add(Task.Run(() => this.SentMetrics()));
            tasks.Add(Task.Run(() => this.GetLastLogOnSceduller()));
            if (monitorServiceRole == MonitorServiceRole.Primary) tasks.Add(Task.Run(() => this.GetLastJobsOnSceduller()));
            Task.WaitAll(tasks.ToArray());
        }
        public void SentMetrics()
        {
            MetriсsProvider MetriksProvider = new MetriсsProvider();
            while (true)
            {
                MetricsModel metricsModel = new MetricsModel(MetriksProvider.GetMetrics(), metricServiceConfiguration.MachineName, DateTime.Now);
                Console.WriteLine(metricsModel);
                Thread.Sleep(metricServiceConfiguration.MetricInterval); 
            }
        }

        public void GetLastJobsOnSceduller()
        {
            JobsCollector jobsCollector = new JobsCollector(metricServiceConfiguration.WallsConnectionString, metricServiceConfiguration.WallsProviderName);
            while (true)
            {
                jobsCollector.GetLastJobs();
                Thread.Sleep(metricServiceConfiguration.JobsInterval);
            }
        }

        public void GetLastLogOnSceduller()
        {
            Parallel.ForEach(metricServiceConfiguration.LogSources, (sourse) =>
            {
                LogCollector logCollector = new LogCollector(sourse.Key, sourse.Value);
                while (true)
                {
                    logCollector.GetLastRows();
                    Thread.Sleep(metricServiceConfiguration.LogsInterval);
                }
            });
        }
    }
}
