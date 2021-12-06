using Contracts.DTOs;
using Domain.Entities;
using Domain.Helpers;
using Domain.Models;
using Domain.Models.QueryParameters;
using Services.Exceptions;
using Services.Repositories;
using Services.Services;
using System;
using System.Linq;
using Xunit;

namespace Services.Tests.ServicesUnitTests
{
    public class RestaurantServiceUnitTests : UnitTests
    {
        private readonly FoodEFRepository _foodRepository;
        private readonly RestaurantEFRepository _restaurantRepository;
        private FoodEntity _foodEntity { get; set; }
        private RestaurantRegisterRequest _restaurantRegisterRequest { get; set; }
        private RestaurantEntity _restaurantEntity { get; set; }

        public RestaurantServiceUnitTests()
        {
            _foodRepository = new FoodEFRepository(_context);
            _restaurantRepository = new RestaurantEFRepository(_context);
            _foodEntity = GetSampleFoodEntity();
            _restaurantRegisterRequest = GetSampleRestaurantRegisterRequest();
            _restaurantEntity = GetSampleRestaurantEntity();
        }

        [Fact]
        public void ChangePass_RestaurantInDataBase_ChangesRestaurantPassword()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var newPassword = new Password("qwerty123!");
            var mail = new Mail(_restaurantEntity.Mail);

            sut.ChangePass(mail, newPassword);

            var restaurant = _context.Restaurants.Find(_restaurantEntity.Id);
            Assert.Equal(mail.Value, restaurant.Mail);
            Assert.False(PasswordHasher.Verify(restaurantPassword, restaurant.Password));
            Assert.Equal(newPassword.Value, restaurant.Password);
        }

        [Fact]
        public void ChangePass_BadMail_ThrowsEntityNotFoundException()
        {
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var newPassword = new Password("qwerty123!");
            var mail = new Mail("newMail@mail.com");

            Assert.Throws<EntityNotFoundException>(() => sut.ChangePass(mail, newPassword));
        }

        [Fact]
        public void ChangePass_PasswordNotValid_ThrowsArgumentException()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var newPassword = new Password("badpassword");
            var mail = new Mail(_restaurantEntity.Mail);

            Assert.Throws<ArgumentException>(() => sut.ChangePass(mail, newPassword));
        }

        [Fact]
        public void GetRestaurantDtoFromMail_RestaurantInDataBase_ReturnsRestaurantDto()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            var restaurantDto = sut.GetRestaurantDtoFromMail(new Mail(_restaurantEntity.Mail));

            Assert.NotNull(restaurantDto);
            Assert.Equal(_restaurantEntity.Id.ToString(), restaurantDto.Id);
        }

        [Fact]
        public void GetRestaurantDtoFromMail_RestaurantWithMailNotInDataBase_ThrowsEntityNotFoundException()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            Assert.Throws<EntityNotFoundException>(() => sut.GetRestaurantDtoFromMail(new Mail("BadMail")));
        }

        [Fact]
        public void DeleteAccount_DeletesRestaurantInDataBase()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            sut.DeleteAccount(new Credentials(_restaurantEntity.Mail, restaurantPassword));

            var restaurant = _context.Restaurants.Find(_restaurantEntity.Id);

            Assert.Null(restaurant);
        }

        [Fact]
        public void DeleteAccount_WrongPassword_ThrowsAuthorizationException()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            Assert.Throws<AuthorizationException>(() => sut.DeleteAccount(new Credentials(_restaurantEntity.Mail, "badPassword")));
        }

        [Fact]
        public void DeleteAccount_WrongMail_ThrowsEntityNotFoundException()
        {
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            Assert.Throws<EntityNotFoundException>(() => sut.DeleteAccount(new Credentials("badMail", "badPassword")));
        }

        [Fact]
        public void GetAllRestaurants_OneRestaurantInDataBaseDefaultParameters_ReturnsPagedListWithRestaurantDtos()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurantDtos = sut.GetAllRestaurants(parameters);

            Assert.Single(restaurantDtos);
            Assert.Equal(_restaurantEntity.Id.ToString(), restaurantDtos.First().Id);
        }

        [Fact]
        public void GetAllRestaurants_MoreThanOneRestaurantInDataBaseDefaultParameters_ReturnsPagedListWithRestaurantDtos()
        {
            _context.Restaurants.Add(_restaurantEntity);
            var otherRestaurant = GetSampleRestaurantEntity();
            var otherRestaurantId = Guid.NewGuid();
            otherRestaurant.Id = otherRestaurantId;
            _context.Restaurants.Add(otherRestaurant);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurantDtos = sut.GetAllRestaurants(parameters);

            Assert.NotEmpty(restaurantDtos);
            Assert.Equal(_restaurantEntity.Id.ToString(), restaurantDtos.First().Id);
            Assert.Equal(otherRestaurantId.ToString(), restaurantDtos[1].Id);
        }

        [Fact]
        public void GetRestaurantById_RestaurantInDataBase_ReturnsRestaurantDto()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            var restaurantDto = sut.GetRestaurantById(_restaurantEntity.Id.ToString());

            Assert.NotNull(restaurantDto);
            Assert.Equal(_restaurantEntity.Id.ToString(), restaurantDto.Id);
        }

        [Fact]
        public void GetRestaurantById_NoRestaurantInDataBase_ThrowsEntityNotFoundException()
        {
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            Assert.Throws<EntityNotFoundException>(() => sut.GetRestaurantById(_restaurantEntity.Id.ToString()));
        }

        [Fact]
        public void GetRestaurantsNear_OneRestaurantNearInDataBaseDefaultParameters_ReturnsPagedListWithRestaurantDto()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurantDto = sut.GetRestaurantsNear(parameters, new Coords(0, 0));

            Assert.Single(restaurantDto);
            Assert.Equal(_restaurantEntity.Id.ToString(), restaurantDto.First().Id);
        }

        [Fact]
        public void GetRestaurantsNear_MoreThanOneRestaurantNearInDataBaseDefaultParameters_ReturnsPagedListWithRestaurantDtos()
        {
            _context.Restaurants.Add(_restaurantEntity);
            var otherRestaurant = GetSampleRestaurantEntity();
            var otherRestaurantId = Guid.NewGuid();
            otherRestaurant.Id = otherRestaurantId;
            _context.Restaurants.Add(otherRestaurant);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurantDtos = sut.GetRestaurantsNear(parameters, new Coords(0, 0));

            Assert.NotEmpty(restaurantDtos);
            Assert.Equal(_restaurantEntity.Id.ToString(), restaurantDtos.First().Id);
            Assert.Equal(otherRestaurantId.ToString(), restaurantDtos[1].Id);
        }

        [Fact]
        public void GetRestaurantsNear_OneRestaurantNearOneRestaurantFarInDataBaseDefaultParameters_ReturnsPagedListWithNearRestaurantDto()
        {
            _context.Restaurants.Add(_restaurantEntity);
            var otherRestaurant = GetSampleRestaurantEntity();
            var otherRestaurantId = Guid.NewGuid();
            otherRestaurant.Id = otherRestaurantId;
            otherRestaurant.Latitude = 10;
            otherRestaurant.Longitude = 10;
            _context.Restaurants.Add(otherRestaurant);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurantDto = sut.GetRestaurantsNear(parameters, new Coords(0, 0));

            Assert.Single(restaurantDto);
            Assert.Equal(_restaurantEntity.Id.ToString(), restaurantDto.First().Id);
        }

        [Fact]
        public void GetRestaurantsNear_NoRestaurantsNearInDataBaseDefaultParameters_ReturnsEmptyPagedList()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurantDto = sut.GetRestaurantsNear(parameters, new Coords(10, 10));

            Assert.Empty(restaurantDto);
        }

        [Fact]
        public void Login_CorrectCredentials_ReturnsTrue()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            Assert.True(sut.Login(new Credentials(_restaurantEntity.Mail, restaurantPassword)));
        }

        [Fact]
        public void Login_IncorrectPassword_ReturnsFalse()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            Assert.False(sut.Login(new Credentials(_restaurantEntity.Mail, "wrongPassword")));
        }

        [Fact]
        public void Login_IncorrectMail_ReturnsFalse()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            Assert.False(sut.Login(new Credentials("wrongMail", restaurantPassword)));
        }

        [Fact]
        public void Register_ValidInput_ReturnsRegisteredRestaurantId()
        {
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var credentials = new Credentials(_restaurantEntity.Mail, restaurantPassword);

            var restaurantId = sut.Register(credentials, _restaurantRegisterRequest);

            var restaurant = _context.Restaurants.Find(Guid.Parse(restaurantId));
            Assert.Equal(_restaurantRegisterRequest.Name, restaurant.Name);
            Assert.Equal(credentials.Mail.Value, restaurant.Mail);
            Assert.True(PasswordHasher.Verify(credentials.Password.Value, restaurant.Password));
        }

        [Fact]
        public void Register_RestaurantMailAlreadyUsed_ThrowsAuthorizationException()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var credentials = new Credentials(_restaurantEntity.Mail, restaurantPassword);

           Assert.Throws<AuthorizationException>(() => sut.Register(credentials, _restaurantRegisterRequest));
        }

        [Fact]
        public void Register_PasswordNotValid_ThrowsArgumentException()
        {
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var credentials = new Credentials(_restaurantEntity.Mail, "badPassword");

            Assert.Throws<ArgumentException>(() => sut.Register(credentials, _restaurantRegisterRequest));
        }

        [Fact]
        public void UpdateRestaurant_UpdateRestaurantInDataBase()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var restaurant = GetSampleRestaurant();
            var newName = "NewTestName";
            restaurant.Name = newName;

            sut.UpdateRestaurant(restaurant);

            var updatedRestaurant = _context.Restaurants.Find(_restaurantEntity.Id);
            Assert.Equal(_restaurantEntity.Id, updatedRestaurant.Id);
            Assert.NotEqual(_restaurantEntity.Name, updatedRestaurant.Name);
        }

        [Fact]
        public void UpdateRestaurant_NoRestaurantInDataBase_ThrowsEntityNotFoundException()
        {
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var restaurant = GetSampleRestaurant();
            var newName = "NewTestName";
            restaurant.Name = newName;

            Assert.Throws<EntityNotFoundException>(() => sut.UpdateRestaurant(restaurant));
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_OneRestaurantFarDefaultParameters_ReturnsRestaurant()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurants = sut.GetAllRestaurantsCloserThan(parameters, new Coords((decimal)0.0001, (decimal)0.0001), Distances.FAR);

            Assert.Single(restaurants);
            Assert.Equal(_restaurantEntity.Id.ToString(), restaurants.ToList()[0].Id);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_MoreOneRestaurantFar_ReturnsRestaurants()
        {
            _context.Restaurants.Add(_restaurantEntity);
            var otherRestaurantId = Guid.NewGuid();
            var otherRestaurant = GetSampleRestaurantEntity();
            otherRestaurant.Id = otherRestaurantId;
            _context.Restaurants.Add(otherRestaurant);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurants = sut.GetAllRestaurantsCloserThan(parameters, new Coords((decimal)0.0001, (decimal)0.0001), Distances.FAR);

            Assert.NotEmpty(restaurants);
            Assert.Equal(_restaurantEntity.Id.ToString(), restaurants.ToList()[0].Id);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[1].Id);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_OneRestaurantVeryFar_ReturnsNothing()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurants = sut.GetAllRestaurantsCloserThan(parameters, new Coords((decimal)1, (decimal)1), Distances.FAR);

            Assert.Empty(restaurants);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_MoreOneRestaurantOneIsFarOneIsMedium_ReturnsMediumRestaurant()
        {
            _context.Restaurants.Add(_restaurantEntity);
            var otherRestaurantId = Guid.NewGuid();
            var otherRestaurantEntity = GetSampleRestaurantEntity();
            otherRestaurantEntity.Id = otherRestaurantId;
            otherRestaurantEntity.Latitude = 1;
            otherRestaurantEntity.Longitude = 1;
            _context.Restaurants.Add(otherRestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurants = sut.GetAllRestaurantsCloserThan(parameters, new Coords((decimal)1.01, (decimal)1.01), Distances.MEDIUM);

            Assert.Single(restaurants);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[0].Id);
        }

        [Fact]
        public void GetAllRestaurantsCloserThan_MoreOneRestaurantOneIsMediumOneIsNear_ReturnsNearRestaurant()
        {
            _context.Restaurants.Add(_restaurantEntity);
            var otherRestaurantId = Guid.NewGuid();
            var otherRestaurantEntity = GetSampleRestaurantEntity();
            otherRestaurantEntity.Id = otherRestaurantId;
            otherRestaurantEntity.Latitude = (decimal)0.1;
            otherRestaurantEntity.Longitude = (decimal)0.1;
            _context.Restaurants.Add(otherRestaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new RestaurantParameters();

            var restaurants = sut.GetAllRestaurantsCloserThan(parameters, new Coords((decimal)0.101, (decimal)0.101), Distances.NEAR);

            Assert.Single(restaurants);
            Assert.Equal(otherRestaurantId.ToString(), restaurants.ToList()[0].Id);
        }

        [Fact]
        public void GetAllFoodFromRestaurant_RestaurantHasFoodDefaultParameters_ReturnsPagedListWithFood()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.Foods.Add(_foodEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new FoodParameters();

            var foods = sut.GetAllFoodFromRestaurant(_restaurantEntity.Id.ToString(), parameters);

            Assert.Single(foods);
            Assert.Equal(_restaurantEntity.Id.ToString(), foods.First().IdRestaurant);
        }

        [Fact]
        public void GetAllFoodFromRestaurant_RestaurantHasNoFoodFDefaultParameters_ReturnsEmptyPagedList()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);
            var parameters = new FoodParameters();

            var foods = sut.GetAllFoodFromRestaurant(_restaurantEntity.Id.ToString(), parameters);

            Assert.Empty(foods);
        }

        [Fact]
        public void GetFoodCountFromRestaurant_RestaurantHasFood_ReturnsFoodCount()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.Foods.Add(_foodEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            var count = sut.GetFoodCountFromRestaurant(_restaurantEntity.Id.ToString());

            Assert.Equal(1, count);
        }

        [Fact]
        public void GetFoodCountFromRestaurant_RestaurantDoesntHaveFood_ReturnsZero()
        {
            _context.Restaurants.Add(_restaurantEntity);
            _context.SaveChanges();
            var sut = new RestaurantService(_restaurantRepository, _foodRepository);

            var count = sut.GetFoodCountFromRestaurant(_restaurantEntity.Id.ToString());

            Assert.Equal(0, count);
        }
    }
}
