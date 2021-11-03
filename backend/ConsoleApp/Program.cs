using System;
using Persistence;
using Persistence.Repositories;
using Services.Services;
using Domain.Entities;
using Domain.Helpers;
using System.Collections.Generic;
using System.Text.Json;
using Contracts.DTOs;
using System.Threading;

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
            Food food = new Food("id",
                "food name",
                15,
                "0",
                new List<TypeOfFood>(),
                1 / 60.0,
                //amountPerInterval:1,
                percentPerInterval: 1);

            Thread.Sleep(2500);

            var foodResp = FoodResponse.FromEntity(food);

            string jsonString = JsonSerializer.Serialize(foodResp);
            Console.WriteLine(jsonString);
        }
    }
}
