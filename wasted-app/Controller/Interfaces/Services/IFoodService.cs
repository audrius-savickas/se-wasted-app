using console_wasted_app.Controller.Entities;
using System.Collections.Generic;

namespace console_wasted_app.Controller.Interfaces
{
    public interface IFoodService
    {
        Food GetFoodById(string id);
        IEnumerable<Food> GetAllFood();
        void RegisterFood(Food food);
        void UpdateFood(Food updatedFood);
        void DeleteFood(string id);
        public Restaurant GetRestaurantOfFood(string idFood);
        public TypeOfFood GetTypeOfFood(string id);
    }
}
