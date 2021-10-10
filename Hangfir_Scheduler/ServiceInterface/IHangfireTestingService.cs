using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfir_Scheduler.ServiceInterface
{
    public interface IHangfireTestingService
    {
        Task<bool> InsertJobAsync(string JobName);
    }
}
