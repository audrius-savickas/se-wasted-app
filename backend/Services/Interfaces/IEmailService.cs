using MimeKit;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        void SendAsync(MimeMessage message);
    }
}
