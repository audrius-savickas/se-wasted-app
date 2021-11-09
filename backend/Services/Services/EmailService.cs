using MailKit.Net.Smtp;
using MimeKit;
using Services.Interfaces;
using System;

namespace Services.Services
{
    public class EmailService : IEmailService
    {
        public async void SendAsync(MimeMessage message)
        {
            using var smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 465, true);
            smtpClient.Authenticate("wasted.app.team@gmail.com", "wastedapp");
            await smtpClient.SendAsync(message);
            smtpClient.Disconnect(true);
        }
    }
}
