using RuilWinkelVaals.Services.EmailService;
using RuilWinkelVaals.Services.EmailService.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuilWinkelVaals.Interfaces
{
    public interface ISendEmailService
    {
        void SendMessage(EmailMessage emailMessage);
    }
}
