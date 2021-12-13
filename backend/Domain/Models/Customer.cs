using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Customer : BaseModel
    {
        public IEnumerable<Reservation> Reservations { get; set; }
        public Credentials Credentials { get; set; }

        public Customer(
            string id,
            string name,
            IEnumerable<Reservation> reservations,
            Credentials credentials) : base(id, name)
        {
            Reservations = reservations;
            Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
        }
    }
}
