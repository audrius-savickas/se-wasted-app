using Domain.Models;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface ITypeOfFoodService
    {
        TypeOfFood GetTypeOfFoodById(string id);
        IEnumerable<TypeOfFood> GetAllTypesOfFood();
        string AddTypeOfFood(TypeOfFood newTypeOfFood);
        void UpdateTypeOfFood(TypeOfFood updatedTypeOfFood);
        void DeleteTypeOfFood(string id);
    }
}
