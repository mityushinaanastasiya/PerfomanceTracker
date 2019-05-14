using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class WallsMonitorDbContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<MetricsModel> MetricsModels { get; set; }
        public WallsMonitorDbContext(DbContextOptions<WallsMonitorDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
