using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public class MetricsModel
    {
        Dictionary<string, string> metrics;
        string machineName;
        DateTime timeStamp; 
        public MetricsModel (Dictionary<string, string> metrics, string machineName, DateTime timeStamp)
        {
            this.metrics = metrics;
            this.machineName = machineName;
            this.timeStamp = timeStamp;
        }
        
        //На данном этапе используется для вывода в консоль 
        public string returnValue (string parametr)
        {
            switch (parametr)
            {
                case "cpu":
                    return this.metrics["cpu"];
                case "ram":
                    return this.metrics["ram"];
                case "timeStamp":
                    return this.timeStamp.ToString();
                case "disk":
                    return this.metrics["disk"];

            }
            return null;
        }
    }
}
