using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RestaurantEntity : Entity 
    {
        public RestaurantEntity()
        {
            FoodItems = new HashSet<FoodEntity>();
        }

        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public virtual ICollection<FoodEntity> FoodItems { get; set; }
    }
}
