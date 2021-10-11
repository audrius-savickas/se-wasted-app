using backend.Controller.Entities;

namespace backend.Controller.Interfaces.Services
{
    public interface IAuthService<T>
    {
        public bool Login(Credentials creds);
        public bool Register(Credentials creds, T obj);
        public void ChangePass(Mail email, Password newPassword);
        public void DeleteAccount(Credentials creds);
    }
}
