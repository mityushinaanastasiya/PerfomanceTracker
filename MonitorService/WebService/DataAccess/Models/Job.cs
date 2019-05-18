using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.DataAccess.Models
{
    public class Job
    {
        public int Id { get; set; }
        public int ExtensionServiceJobsId { get; set; }
        public string ExtensionServiceName { get; set; }
        public string ExtensionType { get; set; }
        public string LibraryName { get; set; }
        public string JobType { get; set; }
        public string JobState { get; set; }
        public string FinalStatus { get; set; }
        public int Retries { get; set; }
        public DateTime QueueTime { get; set; }
        public DateTime StateLastChangedTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Messages { get; set; }
        public Guid OperationId { get; set; }
    }
}
