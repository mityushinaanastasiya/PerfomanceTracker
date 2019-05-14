using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public class Log
    {
        public string NameOfLogSource;
        public List<KeyValuePair<DateTime, string>> Data;
        public Log() { }
        public Log (string nameOfLogSource, List<KeyValuePair<DateTime, string>> data)
        {
            this.NameOfLogSource = nameOfLogSource;
            this.Data = data;
        }
    }
}
