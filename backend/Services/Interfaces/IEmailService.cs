using MimeKit;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(MimeMessage message);
    }
}
