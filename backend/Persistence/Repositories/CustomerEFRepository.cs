using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CustomerEFRepository : ICustomerRepository
    {
        private readonly DatabaseContext _context;
        public CustomerEFRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Delete(string id)
        {
            CustomerEntity entity = GetByIdString(id);
            _context.Customers.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<Customer> GetAll()
        {
            return _context.Customers.Include(x => x.Reservations).Select(x => x.ToDomain());
        }

        public Customer GetById(string id)
        {
            return GetByIdString(id)?.ToDomain();
        }

        public Customer GetByMail(Mail mail)
        {
            return _context.Customers.Include(x => x.Reservations).FirstOrDefault(x => x.Mail == mail.Value)?.ToDomain();
        }

        public string Insert(Customer model)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer model)
        {
            throw new NotImplementedException();
        }

        private CustomerEntity GetByIdString(string id)
        {
            return _context.Customers.Include(x => x.Reservations).FirstOrDefault(x => x.Id == Guid.Parse(id));
        }
    }
}
