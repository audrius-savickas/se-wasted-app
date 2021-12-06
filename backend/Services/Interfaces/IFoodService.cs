using Contracts.DTOs;
using Domain.Models;
using Domain.Models.QueryParameters;
using System;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IFoodService
    {
        Food GetFoodById(string id);
        PagedList<Food> GetAllFood(FoodParameters foodParameters);
        string RegisterFood(Food food);
        void UpdateFood(Food updatedFood);
        void DeleteFood(string idFood, string idRestaurant);
        RestaurantDto GetRestaurantOfFood(string idFood, Coords coords = null);
        IEnumerable<TypeOfFood> GetTypesOfFood(string id);
    }
}
