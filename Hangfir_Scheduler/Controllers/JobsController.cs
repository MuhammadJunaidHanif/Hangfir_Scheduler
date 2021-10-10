using Hangfir_Scheduler.ServiceInterface;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfir_Scheduler.Controllers
{
    [Route("api/scheduler")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IHangfireTestingService  _hangfireTestingService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        //Note: To Read the Documentation visit: https://code-maze.com/hangfire-with-asp-net-core/
        public JobsController(IHangfireTestingService employeeService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _hangfireTestingService = employeeService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet("/ReccuringJob")]
        public ActionResult CreateReccuringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _hangfireTestingService.InsertJobAsync("ReccuringJob"), Cron.Daily());
            return Ok();
        }

        [HttpGet("/ContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            var parentJobId = _backgroundJobClient.Enqueue(() => _hangfireTestingService.InsertJobAsync("ContinuationJob"));
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _hangfireTestingService.InsertJobAsync("ContinuationJob"));

            return Ok();
        }
        [HttpGet("/FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _hangfireTestingService.InsertJobAsync("FireAndForgetJob"));
            return Ok();
        }
        [HttpGet("/DelayedJob")]
        public ActionResult CreateDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _hangfireTestingService.InsertJobAsync(@$"Delayed Job Creation Time:{DateTime.Now}"), TimeSpan.FromSeconds(300));
            return Ok();
        }
    }
}
