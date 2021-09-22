using System;
using console_wasted_app.Controller.Entities;

namespace console_wasted_app.Controller.Interfaces.Services
{
    public interface IAuthService
    {
        public bool Login(Credentials creds);
        public bool Register(Credentials creds);
        public void ChangePass(Mail email, Password newPassword);
        public void DeleteAccount(Credentials creds);
    }
}
