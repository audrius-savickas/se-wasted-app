using Domain.Entities;
using Persistence;
using Persistence.Repositories;
using Services.Mappers;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbConfiguration = DBConfiguration.Instance;

            // Initialize repositories
            var typeOfFoodRepository = new TypeOfFoodRepository
            (
                dbConfiguration.PathToTypesOfFoodFile
            );
            var foodRepository = new FoodRepository
            (
                dbConfiguration.PathToFoodsFile
            );
            var restaurantRepository = new RestaurantRepository
            (
                dbConfiguration.PathToRestaurantsFile
            );

            // Initialize services
            var typeOfFoodService = new TypeOfFoodService
            (
                typeOfFoodRepository
            );
            var foodService = new FoodService
            (
                foodRepository,
                restaurantRepository,
                typeOfFoodRepository
            );
            var restaurantService = new RestaurantService
            (
                restaurantRepository,
                foodRepository
            );

            // Usage
            /*var food = foodService.GetAllFood();

            var entity = food.Select(x => x.ToEntity()).ToList();

            var restaurant = restaurantService.GetAllRestaurants();

            Console.WriteLine("success");*/
        }
    }
}
