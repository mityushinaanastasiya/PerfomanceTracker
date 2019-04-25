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

        public MonitorServiceProcess()
        {
            MetriksProvider = new MetriсsProvider();
        }

        public void SentMetrics()
        {
            int i = 0;//потом будет while (true)
            while (i < 10)
            {
                //формируем модель
                MetricServiceConfiguration metricServiceConfiguration = new MetricServiceConfiguration();
                MetricsModel metricsModel = new MetricsModel(MetriksProvider.GetMetrics(), metricServiceConfiguration.MachineName, DateTime.Now);

                //вывод на экран 
                Console.WriteLine(metricsModel);

                //sentModel(metricsModel));
            
                //Задержка в секунду
                Thread.Sleep(metricServiceConfiguration.MetricInterval); 
                i++;
            }
            Console.Read();
        }
    }
}
