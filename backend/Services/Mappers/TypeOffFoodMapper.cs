using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappers
{
    public static class TypeOffFoodMapper
    {
        public static TypeOfFood ToDomain(this TypeOfFoodEntity from)
        {
            return new TypeOfFood(from.Id.ToString(), from.Name);
        }

        public static TypeOfFoodEntity ToEntity(this TypeOfFood from)
        {
            return new TypeOfFoodEntity
            {
                Id = Guid.Parse(from.Id),
                Name = from.Name,
            };
        }
    }
}
