using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TypeOfFoodEntity : Entity
    {
        public TypeOfFoodEntity()
        {
            Foods = new HashSet<FoodEntity>();
        }

        public string Name { get; set; }

        public virtual ICollection<FoodEntity> Foods { get; set; }
    }
}
