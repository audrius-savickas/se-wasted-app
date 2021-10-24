using System.Collections.Generic;
using Domain.Entities;
using Contracts.DTOs;

namespace Services.Interfaces
{
    public interface IFoodService
    {
        Food GetFoodById(string id);
        IEnumerable<Food> GetAllFood();
        string RegisterFood(Food food);
        void UpdateFood(Food updatedFood);
        void DeleteFood(string idFood, string idRestaurant);
        RestaurantDto GetRestaurantOfFood(string idFood);
        TypeOfFood GetTypeOfFood(string id);
    }
}
