using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public class LogModel
    {
        public string NameOfLogSource;
        public string MachineName;
        public List<KeyValuePair<DateTime, string>> Data;
        public LogModel() { }
        public LogModel (string nameOfLogSource, string machineName, List<KeyValuePair<DateTime, string>> data)
        {
            this.NameOfLogSource = nameOfLogSource;
            this.MachineName = machineName;
            this.Data = data;
        }
    }
}
