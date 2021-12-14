using Contracts.DTOs;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Mappers
{
    public static class CustomerMapper
    {
        public static Customer ToDomain(this CustomerEntity from)
        {
            _ = from ?? throw new ArgumentNullException(nameof(from));

            Credentials credentials = new Credentials
            {
                Mail = new Mail { Value = from.Mail },
                Password = new Password { Value = from.Password }
            };

            return new Customer(
                from.Id.ToString(),
                from.FirstName,
                from.LastName,
                from.Reservations.Select(x => x.ToDomain()),
                credentials);
        }

        public static CustomerEntity ToEntity(this Customer from)
        {
            _ = from ?? throw new ArgumentNullException(nameof(from));

            return new CustomerEntity
            {
                Id = Guid.Parse(from.Id),
                FirstName = from.FirstName,
                LastName = from.LastName,
                Mail = from.Credentials.Mail.Value,
                Password = from.Credentials.Password.Value,
            };
        }

        public static CustomerDto ToDTO(this Customer from)
        {
            return new CustomerDto(from.Id, from.FirstName, from.LastName);
        }
    }
}
