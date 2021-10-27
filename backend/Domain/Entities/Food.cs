using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Food : BaseEntity
    {
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdRestaurant { get; set; }
        public virtual IEnumerable<TypeOfFood> TypesOfFood { get; set; }

        public Food() : base() { }

        public Food
        (
            string id,
            string name,
            decimal price,
            string idRestaurant,
            IEnumerable<TypeOfFood> typesOfFood,
            DateTime? createdAt = null
        )
            : base(id, name)
        {
            Price = price;
            CreatedAt = createdAt ?? DateTime.Now;
            IdRestaurant = idRestaurant;
            TypesOfFood = typesOfFood;
        }
    }
}
