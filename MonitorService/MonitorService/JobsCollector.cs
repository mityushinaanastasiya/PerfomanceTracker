using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DbExtensions;
using Models;
namespace MonitorService
{
    class JobsCollector
    {
        private DateTime currentDate;
        private Database db;
        public JobsCollector(string wallsConnectionString, string wallsProviderName)
        {
            db = new Database(wallsConnectionString, wallsProviderName);
            currentDate = DateTime.Now;
        }

        public List<Job> GetLastJobs()
        {
            var query = SQL
                .SELECT(
                    "ExtensionServiceJobsId, ExtensionServiceName, ExtensionType, LibraryName, JobType, JobState, FinalStatus, Retries, QueueTime, StateLastChangedTime, StartTime, EndTime, Messages, OperationId")
                .FROM("ExtensionServiceJobs")
                .WHERE($"(EndTime > '{currentDate}' or StartTime > '{currentDate}') and JobState = 'FINISHED'");
            List<Job> lastJobs = db.Map<Job>(query).ToList();
            if (lastJobs.Any()) currentDate = lastJobs.Max(j => j.QueueTime);
            return lastJobs;
        }

    }
}
