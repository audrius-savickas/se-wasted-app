using System;
using System.Collections.Generic;
using System.Text.Json;
using wasted_app.Controller.Entities;

namespace console_wasted_app.Controller.Entities
{
    public class Food : BaseEntity
    {
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdRestaurant { get; set; }
        public IList<string> ArrIdTypesOfFood { get; set; }

        public Food
        (
            string id,
            string name,
            decimal price,
            string idRestaurant,
            List<string> arrIdTypesOfFood,
            DateTime? createdAt = null
        )
            : base(id, name)
        {
            Price = price;
            CreatedAt = createdAt ?? DateTime.Now;
            IdRestaurant = idRestaurant;
            ArrIdTypesOfFood = arrIdTypesOfFood;
        }

        public Food(JsonElement json) : base(json)
        {
            Price = json.GetProperty("price").GetDecimal();
            CreatedAt = DateTime.Parse(json.GetProperty("createdAt").GetString());
            IdRestaurant = json.GetProperty("idRestaurant").GetString();
        }

        public void AddTypeOfFood(string id)
        {
            ArrIdTypesOfFood.Add(id);
        }

        public void DeleteTypeOfFood(string id)
        {
            ArrIdTypesOfFood.Remove(id);
        }
    }
}
