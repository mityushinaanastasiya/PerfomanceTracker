using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorService
{
    class MetricServiceConfiguration
    {
        public string MachineName {get;set;}
        public int MetricInterval { get; set; }
        public MetricServiceConfiguration ()
        {
            MachineName = this.GetString("MachineName");
            MetricInterval = this.GetInt("MetricInterval")*1000;
        }
        string GetString(string key) => ConfigurationManager.AppSettings.Get(key);
        int GetInt(string key) => Convert.ToInt32(GetString(key));
    }
}
