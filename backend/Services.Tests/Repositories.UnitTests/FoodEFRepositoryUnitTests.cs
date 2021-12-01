using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Mappers;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Services.Tests
{
    public class FoodEFRepositoryUnitTests
    {
        private readonly DatabaseContext _context;

        public FoodEFRepositoryUnitTests()
        {
            DbContextOptionsBuilder<DatabaseContext> dbOptions = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            _context = new DatabaseContext(dbOptions.Options);   
        }

        private Guid foodId = Guid.NewGuid();
        private Guid restaurantId = Guid.NewGuid();
        private Guid typeOfFoodId = Guid.NewGuid();
        private const string foodName = "FoodTestName";
        private const string typeOfFoodName = "TypeTestName";

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
        public void Add_AddFoodToDataBase()
        {
            var sut = new FoodEFRepository(_context);
            var food = GetSampleFood(foodId, typeOfFoodId);

            sut.Add(food);

            List<FoodEntity> foods = _context.Foods.ToList();
            Assert.Single(foods);
            Assert.Equal(food.Id, _context.Foods.Select(x => x.Id).First().ToString());
        }

        [Fact]
        public void Delete_DeletesExistingFood()
        {
            _context.Foods.Add(GetSampleFoodEntity(foodId, typeOfFoodId));
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);

            sut.Delete(foodId.ToString());

            List<FoodEntity> foods = _context.Foods.ToList();
            Assert.Empty(foods);
        }

        [Fact]
        public void GetAll_OneFoodInDataBase_GetsAllExistingFoods()
        {
            _context.Foods.Add(GetSampleFoodEntity(foodId, typeOfFoodId));
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);

            IEnumerable<Food> foods = sut.GetAll();

            Assert.Single(foods);
        }

        [Fact]
        public void GetAll_MoreThanOneFoodInDataBase_GetsAllExistingFoods()
        {
            _context.Foods.Add(GetSampleFoodEntity(foodId, typeOfFoodId));
            var otherFoodId = Guid.NewGuid();
            var otherTypeId = Guid.NewGuid();
            _context.Foods.Add(GetSampleFoodEntity(otherFoodId, otherTypeId));
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);

            IEnumerable<Food> foods = sut.GetAll();

            Assert.NotEmpty(foods);
            Assert.Equal(foodId.ToString(), foods.ToList()[0].Id);
            Assert.Equal(otherFoodId.ToString(), foods.ToList()[1].Id);
        }

        [Fact]
        public void GetById_FoodExistsInDataBase_ReturnsFood()
        {
            _context.Foods.Add(GetSampleFoodEntity(foodId, typeOfFoodId));
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);

            var food = sut.GetById(foodId.ToString());

            Assert.Equal(foodId.ToString(), food.Id);
        }

        [Fact]
        public void GetById_FoodDoesntExistsInDataBase_ReturnsNull()
        {
            var sut = new FoodEFRepository(_context);

            var food = sut.GetById(Guid.NewGuid().ToString());

            Assert.Null(food);
        }

        [Fact]
        public void Update_UpdateExistingFood()
        {
            _context.Foods.Add(GetSampleFoodEntity(foodId, typeOfFoodId));
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);
            var newFoodName = "NewFoodTestName";
            var food = new Food
            {
                Id = foodId.ToString(),
                IdRestaurant = restaurantId.ToString(),
                Name = newFoodName,
                TypesOfFood = GetSampleTypesOfFood(Guid.NewGuid()),
                PercentPerInterval = 1
            };

            sut.Update(food);

            var UpdatedFood = _context.Foods.Find(foodId);
            Assert.Equal(foodId, UpdatedFood.Id);
            Assert.NotEqual(foodName, UpdatedFood.Name);
        }

        [Fact]
        public void Update_UpdateNotExistingFood()
        {
            _context.Foods.Add(GetSampleFoodEntity(foodId, typeOfFoodId));
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);
            var newFoodName = "NewFoodTestName";
            var food = new Food
            {
                Id = Guid.NewGuid().ToString(),
                IdRestaurant = restaurantId.ToString(),
                Name = newFoodName,
                TypesOfFood = GetSampleTypesOfFood(Guid.NewGuid()),
                PercentPerInterval = 1
            };

            sut.Update(food);

            var foodFormDB = _context.Foods.Find(foodId);
            Assert.Equal(GetSampleFoodEntity(foodId, typeOfFoodId).Name, foodFormDB.Name);
        }
    }
}
