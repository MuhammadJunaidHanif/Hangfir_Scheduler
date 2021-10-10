using Hangfir_Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfir_Scheduler.ServiceInterface
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
