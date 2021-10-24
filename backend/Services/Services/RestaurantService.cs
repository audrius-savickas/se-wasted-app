using System.Collections.Generic;
using System.Linq;

using Domain.Entities;
using Persistence.Interfaces;
using Services.Interfaces;
using Domain.Helpers;
using Contracts.DTOs;

namespace Services.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        
        public void ChangePass(Mail mail, Password newPassword)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(mail);
            if(restaurant == null)
            {
                throw new System.Exception("Invalid email.");
            }
            else
            {
                Credentials creds = restaurant.Credentials;
                creds.Password = newPassword;
                _restaurantRepository.Update(restaurant);
            }
            
            
        }
        
        public RestaurantDto GetRestaurantDtoFromMail(Mail mail)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(mail);
            if(restaurant == null)
            {
                throw new System.Exception("Invalid email.");
            }
            else
            {
                return RestaurantDto.FromEntity(restaurant);
            }
            
        }
        
        public void DeleteAccount(Credentials creds)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(creds.Mail);
            if (restaurant == null)
            {
                throw new System.Exception("Invalid credentials.");
            }
            else
            {
                _restaurantRepository.Delete(restaurant.Id);
            }
            
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
                throw new System.Exception("Invalid id.");
            }
            else
            {
                return RestaurantDto.FromEntity(restaurant);
            }
            
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

        public bool Register(Credentials creds, Restaurant restaurant)
        {
            if (_restaurantRepository.GetByMail(creds.Mail) == null)
            {
                string error = Validator.ValidateEmail(creds.Mail.Value) + Validator.ValidatePassword(creds.Password.Value);
                if (error == "")
                {
                    restaurant.Credentials = new Credentials(creds.Mail.Value, PasswordHasher.Hash(creds.Password.Value));
                    _restaurantRepository.Add(restaurant);
                    return true;
                }
                else
                {
                    throw new System.Exception(error);
                }
            }
            else
            {
                throw new System.Exception("There is already an account registered on this mail.");
            }
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Update(restaurant);
        }

        public IEnumerable<RestaurantDto> GetAllRestaurantsCloserThan(Coords coords, Distances distance)
        {
            return _restaurantRepository
                    .GetAllRestaurantsCloserThan(coords, distance)
                    .Select(r => RestaurantDto.FromEntity(r));
        }
    }
}
