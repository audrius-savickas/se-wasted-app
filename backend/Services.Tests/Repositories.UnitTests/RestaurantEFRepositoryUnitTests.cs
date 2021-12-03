﻿using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Services.Tests
{
    public class RestaurantEFRepositoryUnitTests
    {
        private readonly DatabaseContext _context;

        public RestaurantEFRepositoryUnitTests()
        {
            DbContextOptionsBuilder<DatabaseContext> dbOptions = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            _context = new DatabaseContext(dbOptions.Options);
        }

        private Guid restaurantId = Guid.NewGuid();
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

        [Fact]
        public void Insert_InsertRestaurantToDataBase()
        {
            var sut = new RestaurantEFRepository(_context);
            var restaurant = GetSampleRestaurant(restaurantId);

            sut.Insert(restaurant);

            List<RestaurantEntity> restaurants = _context.Restaurants.ToList();
            Assert.Single(restaurants);
            Assert.Equal(restaurant.Id, _context.Restaurants.Select(x => x.Id).First().ToString());
        }

        [Fact]
        public void Delete_DeletesExistingRestaurant()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            sut.Delete(restaurantId.ToString());

            List<RestaurantEntity> restaurants = _context.Restaurants.ToList();
            Assert.Empty(restaurants);
        }

        [Fact]
        public void GetAll_OneRestaurantInDataBase_GetsAllExistingRestaurnats()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IEnumerable<Restaurant> restaurants = sut.GetAll();

            Assert.Single(restaurants);
        }

        [Fact]
        public void GetAll_MoreThanOneRestaurantInDataBase_GetsAllExistingRestaurants()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            var otherRestaurantId = Guid.NewGuid();
            _context.Restaurants.Add(GetSampleRestaurantEntity(otherRestaurantId));
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IEnumerable<Restaurant> restaurants = sut.GetAll();

            Assert.NotEmpty(restaurants);
            Assert.Equal(restaurantId.ToString(), restaurants.ToList()[0].Id);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[1].Id);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_OneRestaurantFar_ReturnsRestaurant()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IEnumerable<Restaurant> restaurants = sut.GetAllRestaurantsCloserThan(new Coords((decimal)0.0001, (decimal)0.0001), Distances.FAR);

            Assert.Single(restaurants);
            Assert.Equal(restaurantId.ToString(), restaurants.ToList()[0].Id);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_MoreOneRestaurantFar_ReturnsRestaurants()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            var otherRestaurantId = Guid.NewGuid();
            _context.Restaurants.Add(GetSampleRestaurantEntity(otherRestaurantId));
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IEnumerable<Restaurant> restaurants = sut.GetAllRestaurantsCloserThan(new Coords((decimal)0.0001, (decimal)0.0001), Distances.FAR);

            Assert.NotEmpty(restaurants);
            Assert.Equal(restaurantId.ToString(), restaurants.ToList()[0].Id);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[1].Id);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_OneRestaurantVeryFar_ReturnsNothing()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IEnumerable<Restaurant> restaurants = sut.GetAllRestaurantsCloserThan(new Coords((decimal)1, (decimal)1), Distances.FAR);

            Assert.Empty(restaurants);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_MoreOneRestaurantOneIsFarOneIsMedium_ReturnsMediumRestaurant()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            var otherRestaurantId = Guid.NewGuid();
            var otherRestaurantEntity = GetSampleRestaurantEntity(otherRestaurantId);
            otherRestaurantEntity.Latitude = 1;
            otherRestaurantEntity.Longitude = 1;
            _context.Restaurants.Add(otherRestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IEnumerable<Restaurant> restaurants = sut.GetAllRestaurantsCloserThan(new Coords((decimal)1.01, (decimal)1.01), Distances.MEDIUM);

            Assert.Single(restaurants);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[0].Id);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_MoreOneRestaurantOneIsMediumOneIsNear_ReturnsNearRestaurant()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            var otherRestaurantId = Guid.NewGuid();
            var otherRestaurantEntity = GetSampleRestaurantEntity(otherRestaurantId);
            otherRestaurantEntity.Latitude = (decimal)0.1;
            otherRestaurantEntity.Longitude = (decimal)0.1;
            _context.Restaurants.Add(otherRestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IEnumerable<Restaurant> restaurants = sut.GetAllRestaurantsCloserThan(new Coords((decimal)0.101, (decimal)0.101), Distances.NEAR);

            Assert.Single(restaurants);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[0].Id);
        }

        [Fact]
        public void GetById_RestaurantExistsInDataBase_ReturnsRestaurant()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            var restaurant = sut.GetById(restaurantId.ToString());

            Assert.Equal(restaurantId.ToString(), restaurant.Id);
        }

        [Fact]
        public void GetById_RestaurantDoesntExistsInDataBase_ReturnsNull()
        {
            var sut = new RestaurantEFRepository(_context);

            var restaurant = sut.GetById(Guid.NewGuid().ToString());

            Assert.Null(restaurant);
        }


        [Fact]
        public void GetByMail_RestaurantExistsInDataBase_ReturnsRestaurant()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            var restaurant = sut.GetByMail(new Mail (restaurantMail));

            Assert.Equal(restaurantMail, restaurant.Credentials.Mail.Value);
        }

        [Fact]
        public void GetByMail_RestaurantDoesntExistsInDataBase_ReturnsNull()
        {
            var sut = new RestaurantEFRepository(_context);

            var restaurant = sut.GetByMail(new Mail(restaurantMail));

            Assert.Null(restaurant);
        }

        [Fact]
        public void Update_UpdateExistingRestaurant()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);
            var newRestaurantName = "NewRestaurantTestName";
            var Restaurant = new Restaurant
            {
                Id = restaurantId.ToString(),
                Name = newRestaurantName,
                Coords = new Coords(0, 0),
                Credentials = new Credentials("mail", "password")
            };

            sut.Update(Restaurant);

            var UpdatedRestaurant = _context.Restaurants.Find(restaurantId);
            Assert.Equal(restaurantId, UpdatedRestaurant.Id);
            Assert.NotEqual(restaurantName, UpdatedRestaurant.Name);
        }

        [Fact]
        public void Update_UpdateNotExistingRestaurant()
        {
            _context.Restaurants.Add(GetSampleRestaurantEntity(restaurantId));
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);
            var newRestaurantName = "NewRestaurantTestName";
            var Restaurant = new Restaurant
            {
                Id = Guid.NewGuid().ToString(),
                Name = newRestaurantName,
                Coords = new Coords(0, 0),
                Credentials = new Credentials("mail", "password")
            };

            sut.Update(Restaurant);

            var RestaurantFormDB = _context.Restaurants.Find(restaurantId);
            Assert.Equal(restaurantName, RestaurantFormDB.Name);
        }
    }
}
