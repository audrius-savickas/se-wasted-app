using Domain.Entities;
using Persistence.File;
using Persistence.File.Repositories;
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

            //var emailService = new EmailService();


            /*for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(Guid.NewGuid());
            }*/



            // Usage
            var food = foodService.GetAllFood();

            var entity = food.Select(x => x.ToEntity()).ToList();

            var restaurant = restaurantService.GetAllRestaurants();

            Console.WriteLine("success");
        }
    }
}
