using System;
using System.Text.Json;
using wasted_app.Controller.Entities;

namespace console_wasted_app.Controller.Entities
{
    public class Food : BaseEntity, ICloneable
    {
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdRestaurant { get; set; }
        public string IdTypeOfFood { get; set; }

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

        public Food(JsonElement json) : base(json)
        {
            Price = json.GetProperty("Price").GetDecimal();
            CreatedAt = DateTime.Parse(json.GetProperty("CreatedAt").GetString());
            IdTypeOfFood = json.GetProperty("IdTypeOfFood").GetString();
            IdRestaurant = json.GetProperty("IdRestaurant").GetString();
        }

        public object Clone()
        {
            return new Food(
                Id,
                Name,
                Price,
                IdRestaurant,
                IdTypeOfFood,
                CreatedAt
            );
        }
    }
}
