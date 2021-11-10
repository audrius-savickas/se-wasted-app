using System.Collections.Generic;
using Domain.Entities;
using Contracts.DTOs;
using System;

namespace Services.Interfaces
{
    public interface IFoodService
    {
        Food GetFoodById(string id);
        IEnumerable<Food> GetAllFood();
        string RegisterFood(Food food, Func<string> generateId);
        void UpdateFood(Food updatedFood);
        void DeleteFood(string idFood, string idRestaurant);
        RestaurantDto GetRestaurantOfFood(string idFood);
        IEnumerable<TypeOfFood> GetTypesOfFood(string id);
    }
}
