using Hangfir_Scheduler.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfir_Scheduler.DBContext
{
    public  class HanfireDbContext : DbContext
    {
        public HanfireDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<HangfireTesting> HangfireTesting { get; set; }
    }
}
