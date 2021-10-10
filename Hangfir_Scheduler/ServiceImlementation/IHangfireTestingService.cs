using Hangfir_Scheduler.DBContext;
using Hangfir_Scheduler.Models;
using Hangfir_Scheduler.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfir_Scheduler.ServiceImlementation
{
    public class HangfireTestingService : IHangfireTestingService
    {
        
        private readonly HanfireDbContext _employeeDbContext;
      
        public HangfireTestingService(HanfireDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
       
        public async Task<bool> InsertJobAsync(string JobName)
        {

            HangfireTesting employee = new HangfireTesting()
            {
                JobName = JobName,
                ExecutionTime = DateTime.Now.ToString()
                };
                await _employeeDbContext.AddAsync(employee);
                await _employeeDbContext.SaveChangesAsync();
                return true;
            
            
        }
    
    }
}
