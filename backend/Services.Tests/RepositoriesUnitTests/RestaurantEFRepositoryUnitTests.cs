using Domain.Entities;
using Domain.Models;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Services.Tests.RepositoriesUnitTests
{
    public class RestaurantEFRepositoryUnitTests : UnitTests
    {
        private Restaurant Restaurant { get; set; }
        private RestaurantEntity RestaurantEntity { get; set; }

        public RestaurantEFRepositoryUnitTests()
        {
            Restaurant = GetSampleRestaurant();
            RestaurantEntity = GetSampleRestaurantEntity();
        }

        [Fact]
        public void Insert_InsertRestaurantToDataBase()
        {
            var sut = new RestaurantEFRepository(_context);

            sut.Insert(Restaurant);

            List<RestaurantEntity> restaurants = _context.Restaurants.ToList();
            Assert.Single(restaurants);
            Assert.Equal(Restaurant.Id, _context.Restaurants.Select(x => x.Id).First().ToString());
        }

        [Fact]
        public void Delete_DeletesExistingRestaurant()
        {
            _context.Restaurants.Add(RestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            sut.Delete(RestaurantEntity.Id.ToString());

            Assert.Empty(_context.Restaurants.ToList());
        }

        [Fact]
        public void GetAll_OneRestaurantInDataBase_GetsAllExistingRestaurnats()
        {
            _context.Restaurants.Add(RestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            Assert.Single(sut.GetAll());
        }

        [Fact]
        public void GetAll_MoreThanOneRestaurantInDataBase_GetsAllExistingRestaurants()
        {
            _context.Restaurants.Add(RestaurantEntity);
            var otherRestaurantId = Guid.NewGuid();
            var otherRestaurant = GetSampleRestaurantEntity();
            otherRestaurant.Id = otherRestaurantId;
            _context.Restaurants.Add(otherRestaurant);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IQueryable<Restaurant> restaurants = sut.GetAll();

            Assert.NotEmpty(restaurants);
            Assert.Equal(RestaurantEntity.Id.ToString(), restaurants.ToList()[0].Id);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[1].Id);
        }
        
        [Fact]
        public void GetAllRestaurantsCloserThan_OneRestaurantFar_ReturnsRestaurant()
        {
            _context.Restaurants.Add(RestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IQueryable<Restaurant> restaurants = sut.GetAllRestaurantsCloserThan(new Coords((decimal)0.0001, (decimal)0.0001), Distances.FAR);

            Assert.Single(restaurants);
            Assert.Equal(RestaurantEntity.Id.ToString(), restaurants.ToList()[0].Id);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_MoreOneRestaurantFar_ReturnsRestaurants()
        {
            _context.Restaurants.Add(RestaurantEntity);
            var otherRestaurantId = Guid.NewGuid();
            var otherRestaurant = GetSampleRestaurantEntity();
            otherRestaurant.Id = otherRestaurantId;
            _context.Restaurants.Add(otherRestaurant);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IQueryable<Restaurant> restaurants = sut.GetAllRestaurantsCloserThan(new Coords((decimal)0.0001, (decimal)0.0001), Distances.FAR);

            Assert.NotEmpty(restaurants);
            Assert.Equal(RestaurantEntity.Id.ToString(), restaurants.ToList()[0].Id);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[1].Id);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_OneRestaurantVeryFar_ReturnsNothing()
        {
            _context.Restaurants.Add(RestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IQueryable<Restaurant> restaurants = sut.GetAllRestaurantsCloserThan(new Coords((decimal)1, (decimal)1), Distances.FAR);

            Assert.Empty(restaurants);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_MoreOneRestaurantOneIsFarOneIsMedium_ReturnsMediumRestaurant()
        {
            _context.Restaurants.Add(RestaurantEntity);
            var otherRestaurantId = Guid.NewGuid();
            var otherRestaurantEntity = RestaurantEntity;
            otherRestaurantEntity.Id = otherRestaurantId;
            otherRestaurantEntity.Latitude = 1;
            otherRestaurantEntity.Longitude = 1;
            _context.Restaurants.Add(otherRestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IQueryable<Restaurant> restaurants = sut.GetAllRestaurantsCloserThan(new Coords((decimal)1.01, (decimal)1.01), Distances.MEDIUM);

            Assert.Single(restaurants);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[0].Id);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_MoreOneRestaurantOneIsMediumOneIsNear_ReturnsNearRestaurant()
        {
            _context.Restaurants.Add(RestaurantEntity);
            var otherRestaurantId = Guid.NewGuid();
            var otherRestaurantEntity = RestaurantEntity;
            otherRestaurantEntity.Id = otherRestaurantId;
            otherRestaurantEntity.Latitude = (decimal)0.1;
            otherRestaurantEntity.Longitude = (decimal)0.1;
            _context.Restaurants.Add(otherRestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            IQueryable<Restaurant> restaurants = sut.GetAllRestaurantsCloserThan(new Coords((decimal)0.101, (decimal)0.101), Distances.NEAR);

            Assert.Single(restaurants);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[0].Id);
        }

        [Fact]
        public void GetById_RestaurantExistsInDataBase_ReturnsRestaurant()
        {
            _context.Restaurants.Add(RestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            var restaurant = sut.GetById(RestaurantEntity.Id.ToString());

            Assert.Equal(RestaurantEntity.Id.ToString(), restaurant.Id);
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
            _context.Restaurants.Add(RestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);

            var restaurant = sut.GetByMail(new Mail (RestaurantEntity.Mail));

            Assert.Equal(RestaurantEntity.Mail, restaurant.Credentials.Mail.Value);
            Assert.Equal(RestaurantEntity.Id.ToString(), restaurant.Id);
        }

        [Fact]
        public void GetByMail_RestaurantDoesntExistsInDataBase_ReturnsNull()
        {
            var sut = new RestaurantEFRepository(_context);

            var restaurant = sut.GetByMail(new Mail(RestaurantEntity.Mail));

            Assert.Null(restaurant);
        }

        [Fact]
        public void Update_UpdateExistingRestaurant()
        {
            _context.Restaurants.Add(RestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantEFRepository(_context);
            var newRestaurantName = "NewRestaurantTestName";
            var Restaurant = new Restaurant
            {
                Id = RestaurantEntity.Id.ToString(),
                Name = newRestaurantName,
                Coords = new Coords(0, 0),
                Credentials = new Credentials("mail", "password")
            };

            sut.Update(Restaurant);

            var UpdatedRestaurant = _context.Restaurants.Find(RestaurantEntity.Id);
            Assert.Equal(RestaurantEntity.Id, UpdatedRestaurant.Id);
            Assert.NotEqual(RestaurantEntity.Name, UpdatedRestaurant.Name);
        }

        [Fact]
        public void Update_UpdateNotExistingRestaurant()
        {
            _context.Restaurants.Add(RestaurantEntity);
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

            var RestaurantFormDB = _context.Restaurants.Find(RestaurantEntity.Id);
            Assert.Equal(RestaurantEntity.Name, RestaurantFormDB.Name);
        }
    }
}
