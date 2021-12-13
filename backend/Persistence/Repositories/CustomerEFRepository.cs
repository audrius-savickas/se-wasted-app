using Domain.Models;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CustomerEFRepository : ICustomerRepository
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Customer GetByMail(Mail mail)
        {
            throw new NotImplementedException();
        }

        public string Insert(Customer model)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer model)
        {
            throw new NotImplementedException();
        }
    }
}
