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
            logCollector = new LogCollector(metricServiceConfiguration.LogSources.ElementAt(0).Key, metricServiceConfiguration.LogSources.ElementAt(0).Value);
        }

        public void SentMetrics()
        {
            int i = 0;//потом будет while (true)
            while (i < 10)
            {
                //формируем модель
                 
                MetricsModel metricsModel = new MetricsModel(MetriksProvider.GetMetrics(), metricServiceConfiguration.MachineName, DateTime.Now);

                //вывод на экран 
                Console.WriteLine(metricsModel);
                logCollector.GetLastRows();
                //sentModel(metricsModel));
            
                //Задержка в секунду
                Thread.Sleep(metricServiceConfiguration.MetricInterval); 
                i++;
            }
            Console.Read();
        }
    }
}
