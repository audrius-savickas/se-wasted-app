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

            //var emailService = new EmailService();


            /*for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(Guid.NewGuid());
            }*/



            // Usage
            var food = foodService.GetAllFood().First();
            food.Id = new Guid().ToString();
            food.IdRestaurant = new Guid().ToString();

            var entity = food.ToEntity();

            var foods = entity.TypesOfFood.First().Foods;

        }
    }
}
