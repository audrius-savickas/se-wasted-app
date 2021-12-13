using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class CustomerRegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CustomerRegisterRequest(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
