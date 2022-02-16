using Domain.Models;
using System;
using System.Collections.Generic;

namespace Contracts.DTOs
{
    public class FoodResponse : BaseDto
    {
        public FoodResponse(string id, string name) : base(id, name)
        {
        }

        public decimal StartingPrice { get; set; }
        public decimal MinimumPrice { get; set; }
        public string Description { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdRestaurant { get; set; }
        public virtual IEnumerable<TypeOfFood> TypesOfFood { get; set; }
        public DateTime StartDecreasingAt { get; set; }
        public DecreaseType DecreaseType { get; set; }
        public double IntervalTimeInMinutes { get; set; }
        public decimal AmountPerInterval { get; set; }
        public double PercentPerInterval { get; set; }
        public string ImageURL { get; set; }
        public ReservationDto Reservation { get; set; }
    }
}
