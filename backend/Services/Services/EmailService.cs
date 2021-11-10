using MailKit.Net.Smtp;
using MimeKit;
using Services.Interfaces;
using System.Threading.Tasks;

namespace Services.Services
{
    public class EmailService : IEmailService
    {
        private const string WASTED_EMAIL = "wasted.app.team@gmail.com";
        private const string WASTED_PW = "wastedapp";
        public EmailService(IRestaurantService restaurantService)
        {
            // Subscribe to the event
            restaurantService.RestaurantRegistered += async delegate (object sender, RestaurantEventArgs e)
            {
                var email = CreateRegistrationConfirmationEmail(e);
                await SendAsync(email);
            };
        }
        public async Task SendAsync(MimeMessage message)
        {
            using var smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 465, true);
            smtpClient.Authenticate(WASTED_EMAIL, WASTED_PW);
            await smtpClient.SendAsync(message);
            smtpClient.Disconnect(true);
        }

        private static MimeMessage CreateRegistrationConfirmationEmail(RestaurantEventArgs e)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Wasted App Team", WASTED_EMAIL));
            mailMessage.To.Add(MailboxAddress.Parse(e.Restaurant.Credentials.Mail.Value));
            mailMessage.Subject = "Registration confirmation";
            mailMessage.Body = new TextPart("plain")
            {
                Text = "Welcome, " + e.Restaurant.Name + "!"
            };
            return mailMessage;
        }
    }
}
