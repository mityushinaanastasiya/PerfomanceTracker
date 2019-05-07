using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public class MetricsModel
    {
        public Dictionary<string, string> Data { get; set; }
        public string MachineName { get; set; }
        public DateTime TimeStamp { get; set; }
        public MetricsModel ()
        {

        }
        public MetricsModel (Dictionary<string, string> data, string machineName, DateTime timeStamp)
        {
            this.Data = data;
            this.MachineName = machineName;
            this.TimeStamp = timeStamp;
        }

        public override string ToString()
        {
            return $"machine {this.MachineName} CPU {this.Data["cpu"]}% DISK {this.Data["disk"]}% RAM {this.Data["ram"]} % TIMESTAMP {this.TimeStamp}";

        }
    }
}
