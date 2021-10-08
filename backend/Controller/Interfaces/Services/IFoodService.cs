using System.Collections.Generic;
using console_wasted_app.Controller.DTOs;
using console_wasted_app.Controller.Entities;

namespace console_wasted_app.Controller.Interfaces
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
