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
        MetriksProvider metriksProvider; 

        public MonitorServiceProcess()
        {
            metriksProvider = new MetriksProvider();
        }

        public async void sentMetrics()
        {
            int i = 0;//потом будет while (true)
            while (i < 10)
            {
                //формируем модель
                MetricsModel metricsModel = new MetricsModel(metriksProvider.getMetrics(), "machineName", DateTime.Now);

                //вывод на экран 
                Console.WriteLine("cpu "+ metricsModel.returnValue("cpu") + "% "+ "disk " + metricsModel.returnValue("disk") + "% ram" + metricsModel.returnValue("ram") +  "% TimeStamp " + metricsModel.returnValue("timeStamp"));

                //Асинхронная отправка сообщения на сервер (еще не реализовано)
                //await Task.Run(() => sentModel(metricsModel));
            
                //Задержка в секунду
                Thread.Sleep(1000); 
                i++;
            }
            Console.Read();
        }
    }
}
