using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RuilWinkelVaals.Interfaces;
using RuilWinkelVaals.Services.EmailService;
using RuilWinkelVaals.Services.EmailService.Configuration;

namespace RuilWinkelVaals.Services.EmailService
{
    public class PasswordForgottenEmail
    {
        private readonly ISendEmailService sendEmailService;

        public static SendEmailConfiguration GetEmailConfiguration()
        {
            SendEmailConfiguration emailConfiguration = new SendEmailConfiguration()
            {
                SmtpServer = "smtp-relay.sendinblue.com",
                SmtpPort = 587, 
                SmtpUseSsl = true,
                SmtpRequireAuthentication = true,
                SmtpUsername = "1816802crijns@zuyd.nl",
                SmtpPassword = "G9ZXgcqUhJYB7NLE"
            };

            return emailConfiguration;
        }

        public static void SendPasswordForgottenEmail()
        {
            SendEmailConfiguration emailConfiguration = GetEmailConfiguration();
            System.Text.StringBuilder mailcontent = new System.Text.StringBuilder();
            mailcontent.Append("Dit is een testmail van Ruilwinkel Vaals");
            mailcontent.Append(String.Format("<p>Deze e-mail is automatisch gegenereerd"));

            EmailMessage emailMessage = new EmailMessage();
            emailMessage.FromAddresses.Add(new EmailAddress("Ruilwinkel Vaals", "noreply@ruilwinkelvaals.nl"));
            emailMessage.subject = "Testbericht";
            emailMessage.ToAddresses.Add(new EmailAddress("Tom Crijns", "t.crijns@gmail.com"));
            emailMessage.ToAddresses.Add(new EmailAddress("Jacco Dammer", "1850385dammer@zuyd.nl"));
            emailMessage.ToAddresses.Add(new EmailAddress("Laurens Baarda", "1846922baarda@zuyd.nl"));
            emailMessage.ToAddresses.Add(new EmailAddress("Mart Dohmen", "1842781dohmen@zuyd.nl"));
            emailMessage.ToAddresses.Add(new EmailAddress("Marcel van de Beek", "marcel.vandebeek@zuyd.nl"));
            emailMessage.ToAddresses.Add(new EmailAddress("Tom Crijns", "1816802crijns@zuyd.nl"));
            emailMessage.content = mailcontent.ToString();
            SendEmailService sendEmailService = new SendEmailService(emailConfiguration);
            sendEmailService.SendMessage(emailMessage);
        }
    }
}
