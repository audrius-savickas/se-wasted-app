using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReservationEntity : Entity
    {
        public DateTime ReservedAt { get; set; }
        public Guid FoodId { get; set; }
        public Guid CustomerId { get; set; }
        public bool IsCancelled { get; set; }
        public decimal Price { get; set; }
        public virtual FoodEntity Food { get; set; }
        public virtual CustomerEntity Customer { get; set; }
    }
}
