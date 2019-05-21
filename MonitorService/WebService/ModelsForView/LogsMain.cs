using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess.Models;

namespace WebService.ModelsForView
{
    public class LogsMain
    {
        public string SearchWord { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Dictionary<string, int> CountOfLogs { get; set; }
        public List<Log> AllLogs { get; set; }
        public LogsMain (Dictionary<string, int> countOfLogs, List<Log> allLogs)
        {
            this.CountOfLogs = countOfLogs;
            this.AllLogs = allLogs;
        }
    } 
}
