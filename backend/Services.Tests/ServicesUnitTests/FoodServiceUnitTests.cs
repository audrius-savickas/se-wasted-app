using Domain.Entities;
using Domain.Models;
using Domain.Models.QueryParameters;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Interfaces;
using Services.Exceptions;
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
    public class FoodServiceUnitTests : UnitTests
    {
        private readonly FoodEFRepository _foodRepository;
        private readonly RestaurantEFRepository _restaurantRepository;
        private readonly TypeOfFoodEFRepository _typeOfFoodRepository;
        private FoodEntity _foodEntity { get; set; }
        private Food _food { get; set; }
        private Restaurant _restaurant { get; set; }
        private RestaurantEntity _restaurantEntity { get; set; }
        private TypeOfFood _typeOfFood { get; set; }
        private TypeOfFoodEntity _typeOfFoodEntity { get; set; }

        public FoodServiceUnitTests()
        {
            _foodRepository = new FoodEFRepository(_context);
            _restaurantRepository = new RestaurantEFRepository(_context);
            _typeOfFoodRepository = new TypeOfFoodEFRepository(_context);
            _foodEntity = GetSampleFoodEntity();
            _food = GetSampleFood();
            _restaurant = GetSampleRestaurant();
            _restaurantEntity = GetSampleRestaurantEntity();
            _typeOfFood = GetSampleTypeOfFood();
            _typeOfFoodEntity = GetSampleTypeOfFoodEntity();
        }

        [Fact]
        public void DeleteFood_FoodInDataBase_DeletesFood()
        {
            _context.Foods.Add(_foodEntity);
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new FoodService(_foodRepository, _restaurantRepository, _typeOfFoodRepository);

            sut.DeleteFood(_food.Id.ToString(), _restaurant.Id.ToString());

            Assert.Empty(_context.Foods.ToList());
        }

        [Fact]
        public void DeleteFood_FoodInDataBaseWrongRestaurant_ReturnsAuthorizationExeption()
        {
            _context.Foods.Add(_foodEntity);
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new FoodService(_foodRepository, _restaurantRepository, _typeOfFoodRepository);

            Assert.Throws<AuthorizationException>(() => sut.DeleteFood(_food.Id.ToString(), Guid.NewGuid().ToString()));
        }

        [Fact]
        public void DeleteFood_DeleteNotExistingFood_ReturnsEntityNotFoundException()
        {
            _context.Foods.Add(_foodEntity);
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new FoodService(_foodRepository, _restaurantRepository, _typeOfFoodRepository);

            Assert.Throws<EntityNotFoundException>(() => sut.DeleteFood(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
            Assert.Single(_context.Foods.ToList());
        }
        

        [Fact]
        public void GetAllFood_OneFoodInDataBase_ReturnsPagedListWithFood()
        {
            _context.Foods.Add(_foodEntity);
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new FoodService(_foodRepository, _restaurantRepository, _typeOfFoodRepository);
            var parameters = new FoodParameters();

            var foods = sut.GetAllFood(parameters);

            Assert.Single(foods);
        }

        [Fact]
        public void GetAllFood_MoreThanOneFoodInDataBase_ReturnsPagedListWithFood()
        {
            _context.Foods.Add(_foodEntity);
            _context.Restaurants.Add(_restaurantEntity);
            var otherFood = GetSampleFoodEntity();
            var otherFoodId = Guid.NewGuid();
            var typeId = Guid.NewGuid();
            otherFood.Id = otherFoodId;
            otherFood.TypesOfFood.First().Id = typeId;
            _context.Foods.Add(otherFood);
            _context.SaveChanges();
            var sut = new FoodService(_foodRepository, _restaurantRepository, _typeOfFoodRepository);
            var parameters = new FoodParameters();

            var foods = sut.GetAllFood(parameters);

            Assert.NotEmpty(foods);
            Assert.Equal(_foodEntity.Id.ToString(), foods.First().Id);
            Assert.Equal(otherFood.Id.ToString(), foods[1].Id);
        }

        [Fact]
        public void GetFoodById_FoodInDataBase_ReturnsFood()
        {
            _context.Foods.Add(_foodEntity);
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new FoodService(_foodRepository, _restaurantRepository, _typeOfFoodRepository);

            var food = sut.GetFoodById(_foodEntity.Id.ToString());

            Assert.Equal(_foodEntity.Id.ToString(), food.Id);
        }

        [Fact]
        public void GetFoodById_NoFoodInDataBase_ReturnsNull()
        {
            var sut = new FoodService(_foodRepository, _restaurantRepository, _typeOfFoodRepository);

            var food = sut.GetFoodById(Guid.NewGuid().ToString());

            Assert.Null(food);
        }

    }
}
