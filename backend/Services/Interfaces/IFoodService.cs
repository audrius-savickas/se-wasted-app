using System.Collections.Generic;
using Domain.Entities;
using Contracts.DTOs;

namespace Services.Interfaces
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
