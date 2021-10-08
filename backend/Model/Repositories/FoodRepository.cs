﻿using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces;
using console_wasted_app.Model.Interfaces;

namespace console_wasted_app.Model.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        public FoodRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }

        public TypeOfFood GetTypeOfFood(string id)
        {
            Food food = GetById(id);
            string idTypeOfFood = food.IdTypeOfFood;

            ServicesController servicesController = ServicesController.Instance;
            ITypeOfFoodService typeOfFoodService = servicesController.TypeOfFoodService;

            return typeOfFoodService.GetByTypeOfFoodId(idTypeOfFood);
        }
    }
}