using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Messages;

namespace MonitorService
{
    class MonitorServiceProcess
    {
        MetriсsProvider MetriksProvider;
        LogCollector logCollector;
        MetricServiceConfiguration metricServiceConfiguration;
        public MonitorServiceProcess()
        {
            MetriksProvider = new MetriсsProvider();
            metricServiceConfiguration = new MetricServiceConfiguration();
            StartMonitoringOfLogs();
        }

        /// <summary>
        /// Запускает процесс мониторинга логов для указанных в конфигурфции экстеншенов в отдельных потоках
        /// </summary>
        void StartMonitoringOfLogs()
        {
            foreach (KeyValuePair <string, string> sourse in metricServiceConfiguration.LogSources)
            {
                Task.Run(() => new LogCollector(sourse.Key, sourse.Value, metricServiceConfiguration.LogsInterval).GetLastRowOnSceduller());
            }
        }

        public void SentMetrics()
        {
            int i = 0;//потом будет while (true)
            while (i < 10)
            {
                MetricsModel metricsModel = new MetricsModel(MetriksProvider.GetMetrics(), metricServiceConfiguration.MachineName, DateTime.Now); 
                Console.WriteLine(metricsModel);
                Thread.Sleep(metricServiceConfiguration.MetricInterval); 
                i++;
            }
            Console.Read();
        }
    }
}
