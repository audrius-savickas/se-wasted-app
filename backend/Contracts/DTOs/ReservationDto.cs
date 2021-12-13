using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class ReservationDto
    {
        public string Id { get; set; }
        public DateTime ReservedAt { get; set; }
        public decimal Price { get; set; }
        public string FoodId { get; set; }
        public string RestaurantId { get; set; }
        public string CustomerId { get; set; }
        public ReservationDto(
            string id,
            string foodId,
            string restaurantId,
            string customerId,
            DateTime reservedAt,
            decimal price)
        {
            Id = id;
            ReservedAt = reservedAt;
            FoodId = foodId;
            RestaurantId = restaurantId;
            Price = price;
            CustomerId = customerId;
        }
    }
}
