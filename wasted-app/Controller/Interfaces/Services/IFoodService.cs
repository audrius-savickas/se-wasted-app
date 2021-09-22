﻿using System;
using System.Collections.Generic;
using console_wasted_app.Controller.Entities;

namespace console_wasted_app.Controller.Interfaces
{
    public interface IFoodService
    {
        Food GetFoodById(string id);
        IEnumerable<Food> GetAllFood();
        void RegisterFood(string idRestaurant, Food food);
        void UpdateFood(Food updatedFood);
        void DeleteFood(string id);
        public Restaurant GetRestaurantOfFood(string idFood);
        public IEnumerable<TypeOfFood> GetTypesOfFood(string id);
    }
}
