using Domain.Entities;

namespace Services.Interfaces
{
    public interface IAuthService<T>
    {
        bool Login(Credentials creds);
        bool Register(Credentials creds, T obj);
        void ChangePass(Mail email, Password newPassword);
        void DeleteAccount(Credentials creds);
    }
}
