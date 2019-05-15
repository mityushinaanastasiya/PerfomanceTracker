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

        public void MonitorProcess()
        {
            tasks.Add(Task.Run(() => this.SentMetrics()));
            //tasks.Add(Task.Run(() => this.GetLastLogOnSceduller()));
            //if (monitorServiceRole == MonitorServiceRole.Primary) tasks.Add(Task.Run(() => this.GetLastJobsOnSceduller()));
            Task.WaitAll(tasks.ToArray());
        }

        public async Task SentMetrics()
        {
            MetriсsProvider MetriksProvider = new MetriсsProvider();
            while (true)
            {
                MetricsModel metricsModel = new MetricsModel(MetriksProvider.GetMetrics(), metricServiceConfiguration.MachineName, DateTime.Now);
                await sentMessages.Send<Messages.MetricsModel>("/api/metrics", metricsModel);
                Thread.Sleep(metricServiceConfiguration.MetricInterval); 
            }
        }

        public async Task GetLastJobsOnSceduller()
        {
            JobsCollector jobsCollector = new JobsCollector(metricServiceConfiguration.WallsConnectionString, metricServiceConfiguration.WallsProviderName);
            while (true)
            {
                await sentMessages.Send<List<Messages.JobModel>>("/api/jobs", jobsCollector.GetLastJobs());
                Thread.Sleep(metricServiceConfiguration.JobsInterval);
            }
        }

        public async Task GetLastLogOnSceduller()
        {
            Parallel.ForEach(metricServiceConfiguration.LogSources, async (sourse) =>
            {
                LogCollector logCollector = new LogCollector(sourse.Value);
                while (true)
                {
                    LogModel log = new LogModel(sourse.Key, logCollector.GetLastRows());
                    await sentMessages.Send<Messages.LogModel>("/api/logs", log);
                    Thread.Sleep(metricServiceConfiguration.LogsInterval);
                }
            });
        }
    }
}
