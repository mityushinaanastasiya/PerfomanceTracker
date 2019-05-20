using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Messages;
using System.IO;
using System.Web.Script.Serialization;

namespace MonitorService
{
    class MonitorServiceProcess
    {
        enum MonitorServiceRole { Primary, Secondary}

        MetricServiceConfiguration metricServiceConfiguration;
        private List<Task> tasks;
        private MonitorServiceRole monitorServiceRole;
        private SentMessages sentMessages;
        public MonitorServiceProcess()
        {  
            metricServiceConfiguration = new MetricServiceConfiguration();
            MonitorServiceRole.TryParse(metricServiceConfiguration.MonitorServiseRole, true, out monitorServiceRole);
            tasks = new List<Task>();
            sentMessages = new SentMessages(metricServiceConfiguration.ServerURL);
        }

        public void Run ()
        {
            //tasks.Add(Task.Run(() => this.WatchMetrics()));
            tasks.Add(Task.Run(() => this.WatchLogs()));
            //if (monitorServiceRole == MonitorServiceRole.Primary) tasks.Add(Task.Run(() => this.WatchJobs()));
            Task.WaitAll(tasks.ToArray());
        }

        public async Task WatchMetrics()
        {
            MetriсsProvider MetriksProvider = new MetriсsProvider();
            while (true)
            {
                MetricsModel metricsModel = new MetricsModel(MetriksProvider.GetMetrics(), metricServiceConfiguration.MachineName, DateTime.Now);
                await sentMessages.SendMetrics(metricsModel);
                Thread.Sleep(metricServiceConfiguration.MetricInterval); 
            }
        }

        public async Task WatchJobs()
        {
            JobsCollector jobsCollector = new JobsCollector(metricServiceConfiguration.WallsConnectionString, metricServiceConfiguration.WallsProviderName);
            while (true)
            {
                await sentMessages.SendJobs(jobsCollector.GetLastJobs());
                Thread.Sleep(metricServiceConfiguration.JobsInterval);
            }
        }

        public async Task WatchLogs()
        {
            Parallel.ForEach(metricServiceConfiguration.LogSources, async (sourse) =>
            {
                LogCollector logCollector = new LogCollector(sourse.Value);
                while (true)
                {
                    LogModel log = new LogModel(sourse.Key, metricServiceConfiguration.MachineName, logCollector.GetLastRows());
                    await sentMessages.SendLogs(log);
                    Thread.Sleep(metricServiceConfiguration.LogsInterval);
                }
            });
        }
    }
}
