using System.Collections.Generic;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface ITypeOfFoodService
    {
        TypeOfFood GetTypeOfFoodById(string id);
        IEnumerable<TypeOfFood> GetAllTypesOfFood();
        void AddTypeOfFood(TypeOfFood newTypeOfFood);
        void UpdateTypeOfFood(TypeOfFood updatedTypeOfFood);
        void DeleteTypeOfFood(string id);
    }
}
