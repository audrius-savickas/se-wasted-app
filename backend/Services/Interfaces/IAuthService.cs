using Domain.Entities;
using System;

namespace Services.Interfaces
{
    public interface IAuthService<T>
    {
        bool Login(Credentials creds);
        string Register(Credentials creds, T obj, Func<string> generateId);
        void ChangePass(Mail email, Password newPassword);
        void DeleteAccount(Credentials creds);
    }
}
