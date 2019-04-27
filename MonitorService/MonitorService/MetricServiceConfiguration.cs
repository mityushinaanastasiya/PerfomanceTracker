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
        public int LogsInterval { get; set; }
        public Dictionary<string, string> LogSources { get; set; } 
        public MetricServiceConfiguration ()
        {
            MachineName = this.GetString("MachineName");
            MetricInterval = this.GetInt("MetricInterval")*1000;
            LogsInterval = this.GetInt("LogsInterval") * 1000;
            LogSources = this.GetDictionary();
        }
        string GetString(string key) => ConfigurationManager.AppSettings.Get(key);
        int GetInt(string key) => Convert.ToInt32(GetString(key));
        Dictionary<string, string> GetDictionary ()
        {
            Dictionary<string, string> logSources = new Dictionary<string, string>();
            var section = (NameValueCollection)ConfigurationManager.GetSection("logsources");
            foreach (string key in section)
            {
                logSources.Add(key, section[key]);
            }
            return logSources;
        }
    }
}
