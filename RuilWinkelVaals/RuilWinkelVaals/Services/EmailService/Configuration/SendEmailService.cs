using RuilWinkelVaals.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;

namespace RuilWinkelVaals.Services.EmailService.Configuration
{
    public class SendEmailService : ISendEmailService
    {
        private readonly SendEmailConfiguration configuration;

        public SendEmailService(SendEmailConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Method which will actually send the message
        /// </summary>
        /// <param name="emailMessage">Emailmessage to send</param>
        public void SendMessage(EmailMessage emailMessage)
        {
            MailMessage mailMessage = new MailMessage();

            if (emailMessage.FromAddresses.Count > 0)
            {
                mailMessage.From = emailMessage.FromAddresses.Select(x => new MailAddress(x.address, x.name)).First();
            }
            else
            {
                mailMessage.From = new MailAddress("1816802crijns@zuyd.nl");
            }

            mailMessage.Priority = MailPriority.Normal;
            mailMessage.Subject = emailMessage.subject;
            mailMessage.IsBodyHtml = true;

            mailMessage.Body = emailMessage.content;

            foreach (var address in emailMessage.ToAddresses.Select(x => new MailAddress(x.address, x.name)))
            {
                mailMessage.To.Add(address);
            }

            foreach (var address in emailMessage.BccAddresses.Select(x => new MailAddress(x.address, x.name)))
            {
                mailMessage.Bcc.Add(address);
            }

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = configuration.SmtpServer;
            smtpClient.Port = configuration.SmtpPort;
            if (configuration.SmtpRequireAuthentication)
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(configuration.SmtpUsername, configuration.SmtpPassword);
            }
            smtpClient.EnableSsl = configuration.SmtpUseSsl;

            smtpClient.Send(mailMessage);
        }


        }
    }
