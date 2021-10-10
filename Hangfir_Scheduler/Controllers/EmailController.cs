using Hangfir_Scheduler.Models;
using Hangfir_Scheduler.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfir_Scheduler.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class EmailController : Controller
        {
            private readonly IMailService mailService;
            public EmailController(IMailService mailService)
            {
                this.mailService = mailService;
            }

            [HttpPost("Send")]
            public async Task<IActionResult> Send([FromForm] MailRequest request)
            {

                await mailService.SendEmailAsync(request);
                return Ok();


            }


        }
    
}