using MimeKit;

namespace Services.Interfaces
{
    interface IEmailService
    {
        void Send(MimeMessage message);
    }
}
