using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using Persistence.Mappers;
using Persistence.Utils;
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

        public string Insert(Customer customer)
        {
            customer.Id = IdGenerator.GenerateUniqueId();

            _context.Customers.Add(customer.ToEntity());
            _context.SaveChanges();
            return customer.Id;
        }

        public void Update(Customer customer)
        {
            if (GetByIdString(customer.Id) == null) return;

            var local = _context.Customers.Local.FirstOrDefault(x => x.Id == Guid.Parse(customer.Id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Customers.Update(customer.ToEntity());

            _context.SaveChanges();
        }

        private CustomerEntity GetByIdString(string id)
        {
            return _context.Customers.Include(x => x.Reservations).FirstOrDefault(x => x.Id == Guid.Parse(id));
        }
    }
}
