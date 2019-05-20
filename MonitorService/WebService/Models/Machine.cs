using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class Machine
    {
        public string Name { get; set; }
        public List<string> LogsSource { get; set; }
        public Machine()
        {

        }
        public Machine(string name, List<string> logsSource)
        {
            this.Name = name;
            this.LogsSource = logsSource;
        }
    }
}
