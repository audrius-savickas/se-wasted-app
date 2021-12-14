using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class CustomerDto : BaseDto
    {
        public override string Name
        {
            get => FirstName + " " + LastName;
            set
            {
                FirstName = value.Split(" ").FirstOrDefault();
                LastName = value.Split(" ").LastOrDefault();
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }

        public CustomerDto(string id, string firstName, string lastName, string mail, string phone) : base(id, firstName + " " + lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Mail = mail;
            Phone = phone;
        }
    }
}
