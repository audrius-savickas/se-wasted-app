using MailKit.Net.Smtp;
using MimeKit;
using Services.Interfaces;
using System;

namespace Services.Services
{
    public class EmailService : IEmailService
    {
        public void Send(MimeMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate("wasted.app.team@gmail.com", "wastedapp");
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }
        }
    }
}
