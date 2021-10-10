using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfir_Scheduler.Models
{
    public class HangfireTesting
    {
        [Key]
        public int Id { get; set; }
        public string JobName { get; set; }
        public string ExecutionTime { get; set; }
    }
}
