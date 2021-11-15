using Contracts.DTOs;
using Domain.Entities;
using Domain.Helpers;
using Persistence.Interfaces;
using Services.Exceptions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IFoodRepository _foodRepository;

        public event EventHandler<RestaurantEventArgs> RestaurantRegistered;

        public RestaurantService
        (
            IRestaurantRepository restaurantRepository,
            IFoodRepository foodRepository
        )
        {
            _restaurantRepository = restaurantRepository;
            _foodRepository = foodRepository;
        }

        public void ChangePass(Mail mail, Password newPassword)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(mail);
            if (restaurant == null)
            {
                throw new EntityNotFoundException("Invalid email.");
            }

            Credentials creds = restaurant.Credentials;
            creds.Password = newPassword;
            _restaurantRepository.Update(restaurant);
        }

        public RestaurantDto GetRestaurantDtoFromMail(Mail mail)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(mail);
            if (restaurant == null)
            {
                throw new EntityNotFoundException("Invalid email.");
            }

            return RestaurantDto.FromEntity(restaurant);
        }

        public void DeleteAccount(Credentials creds)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(creds.Mail);

            if (restaurant == null)
            {
                throw new EntityNotFoundException("The user does not exist");
            }

            if (!restaurant.Credentials.Equals(creds))
            {
                throw new AuthorizationException("The credentials are not correct");
            }

            _restaurantRepository.Delete(restaurant.Id);
        }

        public IEnumerable<RestaurantDto> GetAllRestaurants()
        {
            return _restaurantRepository
                    .GetAll()
                    .Select(r => RestaurantDto.FromEntity(r));
        }

        public RestaurantDto GetRestaurantById(string idRestaurant)
        {
            Restaurant restaurant = _restaurantRepository.GetById(idRestaurant);
            if (restaurant == null)
            {
                throw new EntityNotFoundException("Invalid id.");
            }

            return RestaurantDto.FromEntity(restaurant);
        }

        public IEnumerable<RestaurantDto> GetRestaurantsNear(Coords coords)
        {
            return _restaurantRepository
                    .GetRestaurantsNear(coords)
                    .Select(r => RestaurantDto.FromEntity(r));
        }

        public bool Login(Credentials creds)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(creds.Mail);
            return restaurant != null && restaurant.Credentials.Mail.Value == creds.Mail.Value && PasswordHasher.Verify(creds.Password.Value, restaurant.Credentials.Password.Value);
        }

        public string Register(Credentials creds, RestaurantRegisterRequest restaurantRegisterRequest, Func<string> generateId)
        {

            // Validations
            if (_restaurantRepository.GetByMail(creds.Mail) != null)
            {
                throw new AuthorizationException("There is already an account registered on this mail");
            }

            string error = Validator.ValidateEmail(creds.Mail.Value) + Validator.ValidatePassword(creds.Password.Value);

            if (error != "")
            {
                throw new ArgumentException(error);
            }

            // Registration
            string id = generateId();

            Restaurant restaurant = new Restaurant
            {
                Id = id,
                Name = restaurantRegisterRequest.Name,
                Address = restaurantRegisterRequest.Address,
                Coords = restaurantRegisterRequest.Coords,
                Credentials = new Credentials(creds.Mail.Value, PasswordHasher.Hash(creds.Password.Value)),
                ImageURL = restaurantRegisterRequest.ImageURL,
                Description = restaurantRegisterRequest.Description,
            };

            // Raise an event that restaurant has registered
            OnRestaurantRegistered(new RestaurantEventArgs(restaurant));

            _restaurantRepository.Add(restaurant);
            return id;
        }

        protected virtual void OnRestaurantRegistered(RestaurantEventArgs e)
        {
            RestaurantRegistered?.Invoke(this, e);
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            Restaurant restaurantDB = _restaurantRepository.GetById(restaurant.Id);

            if (restaurantDB == null)
            {
                throw new EntityNotFoundException("Invalid id.");
            }

            restaurant.Credentials = restaurantDB.Credentials;
            _restaurantRepository.Update(restaurant);
        }

        public IEnumerable<RestaurantDto> GetAllRestaurantsCloserThan(Coords coords, Distances distance)
        {
            return _restaurantRepository
                    .GetAllRestaurantsCloserThan(coords, distance)
                    .Select(r => RestaurantDto.FromEntity(r));
        }

        public IEnumerable<Food> GetAllFoodFromRestaurant(string idRestaurant)
        {
            return _foodRepository
                    .GetAll()
                    .Where(f => f.IdRestaurant == idRestaurant);
        }

        public int GetFoodCountFromRestaurant(string idRestaurant)
        {
            return GetAllFoodFromRestaurant(idRestaurant).Count();
        }
    }

    public class RestaurantEventArgs : EventArgs
    {
        public Restaurant Restaurant { get; set; }

        public RestaurantEventArgs(Restaurant restaurant)
        {
            Restaurant = restaurant;
        }
    }
}
