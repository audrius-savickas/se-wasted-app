using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class ReservationDto
    {
        public DateTime ReservedAt { get; set; }
        public decimal Price { get; set; }
        public string CustomerId { get; set; }
        public string FoodId { get; set; }
        public ReservationDto(
            string foodId,
            string customerId,
            DateTime reservedAt,
            decimal price)
        {
            ReservedAt = reservedAt;
            FoodId = foodId;
            Price = price;
            CustomerId = customerId;
        }
    }
}
