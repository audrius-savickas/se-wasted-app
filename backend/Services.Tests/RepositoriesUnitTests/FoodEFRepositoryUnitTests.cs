using Domain.Entities;
using Domain.Models;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Services.Tests.RepositoriesUnitTests
{
    public class FoodEFRepositoryUnitTests : UnitTests
    {
        private FoodEntity FoodEntity { get; set; }
        private Food Food { get; set; }

        public FoodEFRepositoryUnitTests()
        {
            FoodEntity = GetSampleFoodEntity();
            Food = GetSampleFood();
        }


        [Fact]
        public void Insert_InsertFoodToDataBase()
        {
            var sut = new FoodEFRepository(_context);

            sut.Insert(Food);

            List<FoodEntity> foods = _context.Foods.ToList();
            Assert.Single(foods);
            Assert.Equal(Food.Id, foods.First().Id.ToString());
        }

        [Fact]
        public void Delete_DeletesExistingFood()
        {
            _context.Foods.Add(FoodEntity);
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);

            sut.Delete(FoodEntity.Id.ToString());

            Assert.Empty(_context.Foods.ToList());
        }

        [Fact]
        public void GetAll_OneFoodInDataBase_GetsAllExistingFoods()
        {
            _context.Foods.Add(FoodEntity);
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);

            Assert.Single(sut.GetAll());
        }

        [Fact]
        public void GetAll_MoreThanOneFoodInDataBase_GetsAllExistingFoods()
        {
            _context.Foods.Add(FoodEntity);
            var otherFoodId = Guid.NewGuid();
            var otherTypeId = Guid.NewGuid();
            var otherFood = GetSampleFoodEntity();
            otherFood.Id = otherFoodId;
            otherFood.TypesOfFood.First().Id = otherTypeId;
            _context.Foods.Add(otherFood);
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);

            IQueryable<Food> foods = sut.GetAll();

            Assert.NotEmpty(foods);
            Assert.Equal(FoodEntity.Id.ToString(), foods.ToList()[0].Id);
            Assert.Equal(otherFoodId.ToString(), foods.ToList()[1].Id);
        }

        [Fact]
        public void GetById_FoodExistsInDataBase_ReturnsFood()
        {
            _context.Foods.Add(FoodEntity);
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);

            var food = sut.GetById(FoodEntity.Id.ToString());

            Assert.Equal(FoodEntity.Id.ToString(), food.Id);
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
            _context.Foods.Add(FoodEntity);
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);
            var newFoodName = "NewFoodTestName";
            var food = new Food
            {
                Id = FoodEntity.Id.ToString(),
                IdRestaurant = FoodEntity.RestaurantId.ToString(),
                Name = newFoodName,
                TypesOfFood = GetSampleTypesOfFoodList(),
                PercentPerInterval = 1
            };
            food.TypesOfFood.First().Id = Guid.NewGuid().ToString();

            sut.Update(food);

            var UpdatedFood = _context.Foods.Find(FoodEntity.Id);
            Assert.Equal(FoodEntity.Id, UpdatedFood.Id);
            Assert.NotEqual(FoodEntity.Name, UpdatedFood.Name);
        }

        [Fact]
        public void Update_UpdateNotExistingFood()
        {
            _context.Foods.Add(FoodEntity);
            _context.SaveChanges();
            var sut = new FoodEFRepository(_context);
            var newFoodName = "NewFoodTestName";
            var food = new Food
            {
                Id = Guid.NewGuid().ToString(),
                IdRestaurant = FoodEntity.Id.ToString(),
                Name = newFoodName,
                TypesOfFood = GetSampleTypesOfFoodList(),
                PercentPerInterval = 1
            };

            sut.Update(food);

            var foodFormDB = _context.Foods.Find(FoodEntity.Id);
            Assert.Equal(FoodEntity.Name, foodFormDB.Name);
        }
    }
}
