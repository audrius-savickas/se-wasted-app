using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Services.Interfaces;
using Services.Options;
using System.IO;
using System.Threading.Tasks;

namespace Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;

        public EmailService(IRestaurantService restaurantService, IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;

            // Subscribe to the event
            restaurantService.RestaurantRegistered += async delegate (object sender, RestaurantEventArgs e)
            {
                MimeMessage email = CreateRegistrationConfirmationEmail(e);
                await SendAsync(email);
            };
        }

        public async Task SendAsync(MimeMessage message)
        {
            using SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect(_emailOptions.Host, _emailOptions.Port, true);
            smtpClient.Authenticate(_emailOptions.UserName, _emailOptions.Password);
            await smtpClient.SendAsync(message);
            smtpClient.Disconnect(true);
        }

        private MimeMessage CreateRegistrationConfirmationEmail(RestaurantEventArgs e)
        {
            MimeMessage mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Wasted App Team", _emailOptions.UserName));
            mailMessage.To.Add(MailboxAddress.Parse(e.Restaurant.Credentials.Mail.Value));
            mailMessage.Subject = "Registration confirmation";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = File.ReadAllText("../Services/Utils/welcome.html");

            mailMessage.Body = bodyBuilder.ToMessageBody();
            return mailMessage;
        }
    }
}
