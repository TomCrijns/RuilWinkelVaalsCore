using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RuilWinkelVaals.Services.EmailService.Configuration
{
    public class EmailAddress
    {
        public string name { get; set; }
        public string address { get; set; }

        public EmailAddress(string name, string address)
        {
            this.name = name;
            this.address = address;
        }
    }

    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
            BccAddresses = new List<EmailAddress>();
            FromAddresses = new List<EmailAddress>();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress> BccAddresses { get; set; }
        public List<EmailAddress> FromAddresses { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public MailAddress From { get; internal set; }
    }
}
