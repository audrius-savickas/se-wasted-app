using Contracts.DTOs;
using Domain.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CustomerService : ICustomerService
    {
        public void ChangePass(Mail email, Password newPassword)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccount(Credentials creds)
        {
            throw new NotImplementedException();
        }

        public bool Login(Credentials creds)
        {
            throw new NotImplementedException();
        }

        public string Register(Credentials creds, CustomerRegisterRequest obj)
        {
            throw new NotImplementedException();
        }
    }
}
