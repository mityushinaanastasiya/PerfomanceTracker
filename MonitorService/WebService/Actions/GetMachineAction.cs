using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess;
using WebService.Models;
using WebService.ModelsForView;

namespace WebService.Actions
{
    public class GetMachineAction : Action
    {
        public GetMachineAction (WallsMonitorDbContext dbContext) : base(dbContext)
        {

        }
        public List<MachineMain> GetMachines()
        {
            var machineList = dbContext.Metrics
                    .GroupBy(g => g.MachineName)
                    .Select(s => new { MachineName = s.Key });
            List<MachineMain> machines = new List<MachineMain>();
            foreach (var m in machineList)
            {
                MachineMain machine = new MachineMain();
                machine.Name = m.MachineName;
                machine.LastCPU = float.Parse(dbContext.Metrics.Last().Processor);
                machine.LastMemory = float.Parse(dbContext.Metrics.Last().Memory);
                machine.LastPhysicalDisk = float.Parse(dbContext.Metrics.Last().PhysicalDisk);
                var logSourse = dbContext.Logs
                                        .Where(c => c.MachineName == machine.Name)
                                        .GroupBy(g=>g.LogSource)
                                        .Select(s=> new { LogSource = s.Key }).ToList();
                foreach (var ls in logSourse)
                {
                    machine.LogsSource.Add(ls.LogSource);
                }
                machines.Add(machine);
            }
            return machines;
        }

    }
}
