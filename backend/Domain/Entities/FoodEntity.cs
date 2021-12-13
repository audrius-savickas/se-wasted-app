using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FoodEntity : Entity
    {
        public FoodEntity()
        {
            TypesOfFood = new HashSet<TypeOfFoodEntity>();
        }
        public string Name { get; set; }
        public Guid RestaurantId { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal MinimumPrice { get; set; }
        public DateTime StartDecreasingAt { get; set; }
        public double IntervalTimeInMinutes { get; set; }
        public string DecreaseType { get; set; }
        public decimal? AmountPerInterval { get; set; }
        public double? PercentPerInterval { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        public virtual RestaurantEntity Restaurant { get; set; }
        public virtual ReservationEntity Reservation { get; set; }
        public virtual ICollection<TypeOfFoodEntity> TypesOfFood { get; set; }
    }
}
