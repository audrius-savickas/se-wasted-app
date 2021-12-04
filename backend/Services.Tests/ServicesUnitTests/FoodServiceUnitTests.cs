using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Interfaces;
using Services.Mappers;
using Services.Repositories;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Services.Tests.ServicesUnitTests
{
    public class FoodServiceUnitTests
    {
        private readonly DatabaseContext _context;
        private readonly FoodEFRepository _foodRepository;
        private readonly RestaurantEFRepository _restaurantRepository;
        private readonly TypeOfFoodEFRepository _typeOfFoodRepository;
       
        public FoodServiceUnitTests()
        {
            DbContextOptionsBuilder<DatabaseContext> dbOptions = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            _context = new DatabaseContext(dbOptions.Options);
            _foodRepository = new FoodEFRepository(_context);
            _restaurantRepository = new RestaurantEFRepository(_context);
            _typeOfFoodRepository = new TypeOfFoodEFRepository(_context);
        }

        private Guid foodId = Guid.NewGuid();
        private Guid restaurantId = Guid.NewGuid();
        private Guid typeOfFoodId = Guid.NewGuid();
        private const string foodName = "FoodTestName";
        private const string typeOfFoodName = "TypeTestName";
        //private Guid restaurantId = Guid.NewGuid();
        private const string restaurantName = "RestaurantTestName";
        private const string restaurantMail = "mail@mail.com";

        private Restaurant GetSampleRestaurant(Guid id)
        {
            var restaurant = new Restaurant
            {
                Id = id.ToString(),
                Name = restaurantName,
                Coords = new Coords(0, 0),
                Credentials = new Credentials("mail", "password")
            };
            return restaurant;
        }

        private RestaurantEntity GetSampleRestaurantEntity(Guid id)
        {
            var restaurantEntity = new RestaurantEntity
            {
                Id = id,
                Name = restaurantName,
                Mail = restaurantMail,
                Longitude = 0,
                Latitude = 0
            };
            return restaurantEntity;
        }

        private List<TypeOfFood> GetSampleTypesOfFood(Guid id)
        {
            var typeOfFood = new List<TypeOfFood>
            {
                new TypeOfFood
                {
                    Id =id.ToString(),
                    Name = typeOfFoodName
                }
            };
            return typeOfFood;
        }

        private FoodEntity GetSampleFoodEntity(Guid id, Guid typeId)
        {
            var foodEntity = new FoodEntity
            {
                Id = id,
                RestaurantId = restaurantId,
                Name = foodName,
                TypesOfFood = GetSampleTypesOfFood(typeId).Select(x => x.ToEntity()).ToList(),
                PercentPerInterval = 1
            };
            return foodEntity;
        }

        private Food GetSampleFood(Guid id, Guid typeId)
        {
            var food = new Food
            {
                Id = id.ToString(),
                IdRestaurant = restaurantId.ToString(),
                Name = foodName,
                TypesOfFood = GetSampleTypesOfFood(typeId),
                PercentPerInterval = 1
            };
            return food;
        }

        [Fact]
        public void DeleteFood_FoodInDataBase_DeletesFood()
        {
            _context.Foods.Add(GetSampleFoodEntity(foodId, typeOfFoodId));
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            _context.SaveChanges();
            var sut = new FoodService(_foodRepository, _restaurantRepository, _typeOfFoodRepository);

            sut.DeleteFood(foodId.ToString(), restaurantId.ToString());

            List<FoodEntity> foods = _context.Foods.ToList();
            Assert.Empty(foods);
        }
    }
}
