using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccess.Models;

namespace WebService.DataAccess
{
    public class WallsMonitorDbContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Metrics> Metrics { get; set; }
        public WallsMonitorDbContext(DbContextOptions<WallsMonitorDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
