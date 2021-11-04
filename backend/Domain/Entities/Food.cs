using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Food : BaseEntity
    {
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdRestaurant { get; set; }
        public string ImageURL { get; set; }
        public virtual IEnumerable<TypeOfFood> TypesOfFood { get; set; }

        private const string DEFAULT_IMAGE_URL = "https://www.google.lt/url?sa=i&url=https%3A%2F%2Fcommons.wikimedia.org%2Fwiki%2FFile%3ANo-Image-Placeholder.svg&psig=AOvVaw1uR0Zny7PYJn8sTl750JMW&ust=1636147269746000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCKCc6rvS__MCFQAAAAAdAAAAABAD";

        public Food() : base() { }

        public Food
        (
            string id,
            string name,
            decimal price,
            string idRestaurant,
            IEnumerable<TypeOfFood> typesOfFood,
            DateTime? createdAt = null,
            string imageURL = DEFAULT_IMAGE_URL
        )
            : base(id, name)
        {
            Price = price;
            CreatedAt = createdAt ?? DateTime.Now;
            ImageURL = imageURL;
            IdRestaurant = idRestaurant;
            TypesOfFood = typesOfFood;
        }
    }
}
