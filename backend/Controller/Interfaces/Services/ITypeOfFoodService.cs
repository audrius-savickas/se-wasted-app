using System.Collections.Generic;
using backend.Controller.Entities;

namespace backend.Controller.Interfaces
{
    public interface ITypeOfFoodService
    {
        TypeOfFood GetByTypeOfFoodId(string id);
        IEnumerable<TypeOfFood> GetAllTypesOfFood();
        void AddTypeOfFood(TypeOfFood newTypeOfFood);
        void UpdateTypeOfFood(TypeOfFood updatedTypeOfFood);
        void DeleteTypeOfFood(string id);
    }
}
