using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess;
using WebService.Models;

namespace WebService.Actions
{
    public class GetMachineAction : Action
    {
        public GetMachineAction (WallsMonitorDbContext dbContext) : base(dbContext)
        {

        }
        public List<Machine> GetMachines()
        {
            var machineList = dbContext.Metrics
                    .GroupBy(g => g.MachineName)
                    .Select(s => new { MachineName = s.Key });
            List<Machine> machines = new List<Machine>();
            foreach (var m in machineList)
            {
                Machine machine = new Machine();
                machine.Name = m.MachineName;
                var logSourse = dbContext.Logs
                                        .Where(c => c.MachineName == machine.Name)
                                        .GroupBy(g=>g.LogSource)
                                        .Select(s=> new { LogSource = s.Key }).ToList();
                foreach (var ls in logSourse)
                {
                    machine.LogsSource.Add(ls.LogSource);
                }
            }
            return machines;
        }
    }
}
