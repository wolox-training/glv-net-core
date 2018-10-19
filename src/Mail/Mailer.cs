using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace TrainingNet.Mail
{
    public static class Mailer
    {
        private static string Email { get; set; }
        private static string Username { get; set; }
        private static string Password { get; set; }
        private static string Host { get; set; }
        private static int HostPort { get; set; }
        private static string Name { get; set; }

        public static void SetAccountConfiguration(IConfiguration config)
        {
            Email = config["Mailer:Email"];
            Username = config["Mailer:Username"];
            Password = config["Mailer:Password"];
            Host = config["Mailer:Host"];
            HostPort = Convert.ToInt16(config["Mailer:Port"]);
            Name = config["Mailer:Name"];
        }

        public static void Send(string toAddress, string subject, string body)
        {
            SmtpClient client = new SmtpClient(Host, HostPort)
            {
                Credentials = new NetworkCredential(Username, Password),
                EnableSsl = true,
            };
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(Email, Name),
                Body = body,
                Subject = subject,
            };
            mailMessage.To.Add(toAddress);
            client.Send(mailMessage);
        }
    }
}

