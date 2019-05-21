using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess.Models;

namespace WebService.ModelsForView
{
    public class JobMain
    {
        public JobMain(List<Job> jobs)
        {
            this.Jobs = jobs;
        }

        public string SearchWord { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Job> Jobs { get; set; }

    }
}
