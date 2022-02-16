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
        public string FoodId { get; set; }
        public string RestaurantId { get; set; }
        public string CustomerId { get; set; }
        public Reservation(
            string id,
            bool isCancelled,
            string foodId,
            string restaurantId,
            string customerId,
            decimal price,
            DateTime? reservedAt = null) : base(id, null)
        {
            Id = id;
            IsCancelled = isCancelled;
            ReservedAt = reservedAt ?? DateTime.Now;
            FoodId = foodId;
            RestaurantId = restaurantId;
            Price = price;
            CustomerId = customerId;
        }
    }
}
