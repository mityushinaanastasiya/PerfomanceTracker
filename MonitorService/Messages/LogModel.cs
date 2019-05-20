using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public class LogModel
    {
        public string LogSource;
        public string MachineName;
        public List<KeyValuePair<DateTime, string>> Data;
        public LogModel() { }
        public LogModel (string logSource, string machineName, List<KeyValuePair<DateTime, string>> data)
        {
            this.LogSource = logSource;
            this.MachineName = machineName;
            this.Data = data;
        }
    }
}
