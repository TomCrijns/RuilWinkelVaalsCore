
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RuilWinkelVaals.Services.EmailService.Configuration
{
    [DataContract]
    public class SendEmailConfiguration
    {
        private  IConfiguration _configuration;
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpUseSsl { get; set; }
        public bool SmtpRequireAuthentication { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        /// <summary>
        /// Retrieve configurations for sending an automated e-mail
        /// </summary>
        /// <param name="config">config from appsettings.json file</param>
        /// <returns></returns>
        public static SendEmailConfiguration GetEmailConfiguration(IConfiguration config)
        {
            SendEmailConfiguration emailConfiguration = new SendEmailConfiguration()
            {
                SmtpServer = config.GetSection("EmailSettings:SmtpServer").Value,
                SmtpPort = Int32.Parse(config.GetSection("EmailSettings:SmtpPort").Value.ToString()),
                SmtpUseSsl = bool.Parse(config.GetSection("EmailSettings:SmtpUseSsl").Value.ToString()),
                SmtpRequireAuthentication = bool.Parse(config.GetSection("EmailSettings:SmtpRequireAuthentication").Value.ToString()),
                SmtpUsername = config.GetSection("EmailSettings:SmtpUserName").Value,
                SmtpPassword = config.GetSection("EmailSettings:SmtpPassword").Value
            };

            return emailConfiguration;
        }
    }
}
