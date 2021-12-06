using Contracts.DTOs;
using Domain.Entities;
using Domain.Helpers;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
    public class UnitTests
    {
        protected readonly DatabaseContext _context;
        private Guid foodId = Guid.NewGuid();
        private Guid restaurantId = Guid.NewGuid();
        private Guid typeOfFoodId = Guid.NewGuid();
        private const string foodName = "FoodTestName";
        private const string typeOfFoodName = "TypeTestName";
        private const string restaurantName = "RestaurantTestName";
        private const string restaurantMail = "mail@mail.com";
        protected const string restaurantPassword = "Password123!";
        private string RestaurantPasswordHash { get; set; }
        
        public UnitTests()
        {
            RestaurantPasswordHash = PasswordHasher.Hash(restaurantPassword);
            DbContextOptionsBuilder<DatabaseContext> dbOptions = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            _context = new DatabaseContext(dbOptions.Options);
        }

        protected List<TypeOfFood> GetSampleTypesOfFoodList()
        {
            var typeOfFood = new List<TypeOfFood>
            {
                new TypeOfFood
                {
                    Id =typeOfFoodId.ToString(),
                    Name = typeOfFoodName
                }
            };
            return typeOfFood;
        }

        protected FoodEntity GetSampleFoodEntity()
        {
            var foodEntity = new FoodEntity
            {
                Id = foodId,
                RestaurantId = restaurantId,
                Name = foodName,
                TypesOfFood = GetSampleTypesOfFoodList().Select(x => x.ToEntity()).ToList(),
                PercentPerInterval = 1
            };
            return foodEntity;
        }

        protected Food GetSampleFood()
        {
            var food = new Food
            {
                Id = foodId.ToString(),
                IdRestaurant = restaurantId.ToString(),
                Name = foodName,
                TypesOfFood = GetSampleTypesOfFoodList(),
                PercentPerInterval = 1
            };
            return food;
        }

        protected Restaurant GetSampleRestaurant()
        {
            var restaurant = new Restaurant
            {
                Id = restaurantId.ToString(),
                Name = restaurantName,
                Coords = new Coords(0, 0),
                Credentials = new Credentials(restaurantMail, RestaurantPasswordHash)
            };
            return restaurant;
        }

        protected RestaurantEntity GetSampleRestaurantEntity()
        {
            var restaurantEntity = new RestaurantEntity
            {
                Id = restaurantId,
                Name = restaurantName,
                Mail = restaurantMail,
                Password = RestaurantPasswordHash,
                Longitude = 0,
                Latitude = 0
            };
            return restaurantEntity;
        }

        protected RestaurantRegisterRequest GetSampleRestaurantRegisterRequest()
        {
            var restaurantRegisterRequest = new RestaurantRegisterRequest
            (
                restaurantName,
                "adress",
                new Coords(0, 0)
            );
            return restaurantRegisterRequest;
        }

        protected TypeOfFood GetSampleTypeOfFood()
        {
            var typeOfFood = new TypeOfFood
            {
                Id = typeOfFoodId.ToString(),
                Name = typeOfFoodName
            };
            return typeOfFood;
        }

        protected TypeOfFoodEntity GetSampleTypeOfFoodEntity()
        {
            var typeOfFoodEntity = new TypeOfFoodEntity
            {
                Id = typeOfFoodId,
                Name = typeOfFoodName
            };
            return typeOfFoodEntity;
        }
    }
}
