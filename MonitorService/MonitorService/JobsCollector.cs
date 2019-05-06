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
        private int lastJobId;
        private Database db;
        private int jobsInterval;
        public JobsCollector(string wallsConnectionString, string wallsProviderName, int jobsInterval)
        {
            db = new Database(wallsConnectionString, wallsProviderName);
            lastJobId = db.Map<Job>(SQL.SELECT("*").FROM("ExtensionServiceJobs")).First().ExtensionServiceJobsId - 1;
            this.jobsInterval = jobsInterval;
        }

        public List<Job> GetLastJobs()
        {
            var query = SQL
                .SELECT(
                    "ExtensionServiceJobsId, ExtensionServiceName, ExtensionType, LibraryName, JobType, JobState, FinalStatus, Retries, QueueTime, StateLastChangedTime, StartTime, EndTime, Messages, OperationId")
                .FROM("ExtensionServiceJobs")
                .WHERE("ExtensionServiceJobsId >" + lastJobId);
            List<Job> lastJobs = db.Map<Job>(query).ToList();
            lastJobId = lastJobs.Last().ExtensionServiceJobsId;
            return lastJobs;
        }

        public void GetLastJobsOnSceduller()
        {
            while (true)
            {
                GetLastJobs();
                Thread.Sleep(jobsInterval);
            }
        }

    }
}
