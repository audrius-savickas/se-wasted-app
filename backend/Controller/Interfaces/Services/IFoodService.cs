using System.Collections.Generic;
using backend.Controller.DTOs;
using backend.Controller.Entities;

namespace backend.Controller.Interfaces
{
    public interface IFoodService
    {
        Food GetFoodById(string id);
        IEnumerable<Food> GetAllFood();
        void RegisterFood(Food food);
        void UpdateFood(Food updatedFood);
        void DeleteFood(string id);
        RestaurantDto GetRestaurantOfFood(string idFood);
        TypeOfFood GetTypeOfFood(string id);
    }
}
