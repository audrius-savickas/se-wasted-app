using Contracts.DTOs;
using Domain.Helpers;
using Domain.Models;
using Persistence.Interfaces;
using Persistence.Mappers;
using Services.Exceptions;
using Services.Interfaces;
using Services.Mappers;
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
        private readonly IFoodRepository _foodRepository;
        private readonly IReservationRepository _reservationRepository;

        public CustomerService
        (
            ICustomerRepository customerRepository,
            IFoodRepository foodRepository,
            IReservationRepository reservationRepository
        )
        {
            _customerRepository = customerRepository;
            _foodRepository = foodRepository;
            _reservationRepository = reservationRepository;
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
                Credentials = new Credentials(creds.Mail.Value, PasswordHasher.Hash(creds.Password.Value)),
                Phone = customerRegisterRequest.Phone,
            };

            string id = _customerRepository.Insert(customer);
            return id;
        }

        public string GetCustomerIdFromMail(Mail mail)
        {
            return _customerRepository.GetByMail(mail).Id;
        }

        public IEnumerable<FoodResponse> GetReservedFoodFromCustomerId(string customerId)
        {
            var reservations = _reservationRepository.GetAll().ToList();
            var customerReservations = reservations.Where(x => string.Equals(x.CustomerId, customerId, StringComparison.OrdinalIgnoreCase));
            var customerCurrentReservations = customerReservations.Where(x => _foodRepository.GetById(x.FoodId).Reservation != null);
            var foodIds = customerCurrentReservations.Select(x => x.FoodId);

            return _foodRepository.GetAll().ToList().Where(x => foodIds.Contains(x.Id)).Select(x => x.ToFoodResponse());
        }

        public CustomerDto GetCustomerDtoById(string id)
        {
            Customer customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                throw new EntityNotFoundException("Invalid customer id.");
            }

            return customer.ToDTO();
        }
    }
}
