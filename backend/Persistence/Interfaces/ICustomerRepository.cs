using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Customer GetByMail(Mail mail);
    }
}
