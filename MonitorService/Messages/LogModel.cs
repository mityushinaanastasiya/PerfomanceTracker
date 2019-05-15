using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public class LogModel
    {
        public string NameOfLogSource;
        public List<KeyValuePair<DateTime, string>> Data;
        public LogModel() { }
        public LogModel (string nameOfLogSource, List<KeyValuePair<DateTime, string>> data)
        {
            this.NameOfLogSource = nameOfLogSource;
            this.Data = data;
        }
    }
}
