using System;

namespace Domain.Entities
{
    public class Food : BaseEntity
    {
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdRestaurant { get; set; }
        public string IdTypeOfFood { get; set; }

        public Food() : base() { }

        public Food
        (
            string id,
            string name,
            decimal price,
            string idRestaurant,
            string idTypeOfFood,
            DateTime? createdAt = null
        )
            : base(id, name)
        {
            Price = price;
            CreatedAt = createdAt ?? DateTime.Now;
            IdRestaurant = idRestaurant;
            IdTypeOfFood = idTypeOfFood;
        }
    }
}
