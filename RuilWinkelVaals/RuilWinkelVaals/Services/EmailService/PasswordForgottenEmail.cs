using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using RuilWinkelVaals.Interfaces;
using RuilWinkelVaals.Models;
using RuilWinkelVaals.Services.EmailService;
using RuilWinkelVaals.Services.EmailService.Configuration;
using System.Net.Mime;
using System.IO;
using System.Text;
using System.Net;

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

        public static void SendPasswordForgottenEmail(ProfileDatum user, string token)
        {
            SendEmailConfiguration emailConfiguration = GetEmailConfiguration();
            System.Text.StringBuilder mailcontent = new System.Text.StringBuilder();
            
            mailcontent.Append("<img src='https://www.ruilwinkelvaals.nl/ruilwinkelvaals.nl/images/ruilwinkel/logo_ruilwinkel_vaalsWEB.jpg'style='width:20%; height:10%;'>");
            mailcontent.Append("<br />");
            mailcontent.Append("<br />");
            mailcontent.Append(String.Format("<p>Beste {0},</p>", user.Voornaam));
            mailcontent.Append(String.Format("<p>Wat vervelend dat u uw wachtwoord vergeten bent.</p>"));
            mailcontent.Append(String.Format("<p>Via de onderstaande link kunt u uw wachtwoord weer herstellen.</p>"));
            mailcontent.Append(String.Format("<p><em>Deze link is 60 minuten geldig. Bij een ongeldige link kunt u onze <a href='https://localhost:44370/ForgotPassword/ForgotPassword'>wachtwoord herstelpagina</a> bezoeken om een nieuwe herstellink te ontvangen.</em></p>"));
            mailcontent.Append(String.Format("<p><a style='background-color:#8b0000; display:inline-block; color:white; padding:8px 16px; margin:8px 0px;border:none;cursor:pointer;border-radius:8px;font-size:12px;text-transform:uppercase;font-family:Helvetica;' href='https://localhost:44370/ForgotPasswordRestorePassword?email={0}&token={1}'>Reset wachtwoord</a></p>", user.Email, token));
            mailcontent.Append(String.Format("<br />"));
            mailcontent.Append(String.Format("<p><em>Heeft u deze email ontvangen terwijl u uw wachtwoord niet wilt herstellen? Neem dan contact met ons op via het telefoonnummer +31 6 20 74 98 86</em></p>"));
            mailcontent.Append(String.Format("<p>Met vriendelijke groet,</p>"));
            mailcontent.Append(String.Format("<p>Ruilwinkel Vaals</p>"));

            EmailMessage emailMessage = new EmailMessage();
            emailMessage.FromAddresses.Add(new EmailAddress("Ruilwinkel Vaals", "noreply@ruilwinkelvaals.nl"));
            emailMessage.subject = "Wachtwoord herstellen";
            emailMessage.ToAddresses.Add(new EmailAddress("Tom Crijns", "1816802crijns@zuyd.nl"));
            emailMessage.content = mailcontent.ToString();

            SendEmailService sendEmailService = new SendEmailService(emailConfiguration);
            sendEmailService.SendMessage(emailMessage);
        }
    }
}
