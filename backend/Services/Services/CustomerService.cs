using Contracts.DTOs;
using Domain.Helpers;
using Domain.Models;
using Persistence.Interfaces;
using Services.Exceptions;
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
        private readonly ICustomerRepository _customerRepository;

        public event EventHandler<RestaurantEventArgs> RestaurantRegistered;

        public CustomerService
        (
            ICustomerRepository customerRepository
        )
        {
            _customerRepository = customerRepository;
        }

        public void ChangePass(Mail mail, Password newPassword)
        {
            Customer customer = _customerRepository.GetByMail(mail);
            if (customer == null)
            {
                throw new EntityNotFoundException("Invalid email.");
            }

            string error = Validator.ValidatePassword(newPassword.Value);
            if (error != "")
            {
                throw new ArgumentException(error);
            }

            Credentials creds = customer.Credentials;
            newPassword.Value = PasswordHasher.Hash(newPassword.Value);
            creds.Password = newPassword;
            _customerRepository.Update(customer);
        }

        public void DeleteAccount(Credentials creds)
        {
            Customer customer = _customerRepository.GetByMail(creds.Mail);

            if (customer == null)
            {
                throw new EntityNotFoundException("The restaurant does not exist");
            }

            if (!PasswordHasher.Verify(creds.Password.Value, customer.Credentials.Password.Value))
            {
                throw new AuthorizationException("The credentials are not correct");
            }

        }

        public bool Login(Credentials creds)
        {
            Customer customer = _customerRepository.GetByMail(creds.Mail);
            return customer != null 
                && customer.Credentials.Mail.Value == creds.Mail.Value 
                && PasswordHasher.Verify(creds.Password.Value, customer.Credentials.Password.Value);
        }

        public string Register(Credentials creds, CustomerRegisterRequest customerRegisterRequest)
        {
            // Validations
            if (_customerRepository.GetByMail(creds.Mail) != null)
            {
                throw new AuthorizationException("There is already an account registered on this mail");
            }

            string error = Validator.ValidateEmail(creds.Mail.Value) + Validator.ValidatePassword(creds.Password.Value);

            if (error != "")
            {
                throw new ArgumentException(error);
            }

            Customer customer = new Customer
            {
                FirstName = customerRegisterRequest.FirstName,
                LastName = customerRegisterRequest.LastName,
            };

            string id = _customerRepository.Insert(customer);
            return id;
        }
    }
}
