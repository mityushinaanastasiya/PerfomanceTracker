using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DbExtensions;
using Messages;
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

        public List<JobModel> GetLastJobs()
        {
            var query = SQL
                .SELECT(
                    "ExtensionServiceJobsId, ExtensionServiceName, ExtensionType, LibraryName, JobType, JobState, FinalStatus, Retries, QueueTime, StateLastChangedTime, StartTime, EndTime, Messages, OperationId")
                .FROM("ExtensionServiceJobs")
                .WHERE($"QueueTime > '{currentDate.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}' and JobState = 'FINISHED'");
            List<JobModel> lastJobs = db.Map<JobModel>(query).ToList();
            if (lastJobs.Any()) currentDate = lastJobs.Max(j => j.QueueTime);
            return lastJobs;
        }

    }
}
