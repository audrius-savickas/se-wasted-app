using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Customer : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
        public Credentials Credentials { get; set; }

        public Customer(
            string id,
            string firstName,
            string lastName,
            IEnumerable<Reservation> reservations,
            Credentials credentials)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Reservations = reservations;
            Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
        }
    }
}
