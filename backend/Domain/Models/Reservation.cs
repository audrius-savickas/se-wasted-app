using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Reservation : BaseModel
    {
        public DateTime ReservedAt { get; set; }
        public bool IsCancelled { get; set; }
        public decimal Price { get; set; }
        public Food Food { get; set; }
        public Restaurant Restaurant { get; set; }
        public Customer Customer { get; set; }
        public Reservation(
            string id,
            bool isCancelled,
            Food food,
            Restaurant restaurant,
            Customer customer,
            DateTime? reservedAt = null) : base(id, null)
        {
            Id = id;
            IsCancelled = isCancelled;
            ReservedAt = reservedAt ?? DateTime.Now;
            Food = food;
            Restaurant = restaurant;
            Price = food.CalculateCurrentPrice();
            Customer = customer;
        }
    }
}
