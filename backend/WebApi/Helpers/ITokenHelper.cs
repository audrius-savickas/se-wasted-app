using Domain.Models;

namespace WebApi.Helpers
{
    public interface ITokenHelper
    {
        dynamic GenerateToken(Mail mail);
    }
}
