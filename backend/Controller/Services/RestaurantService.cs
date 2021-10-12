using System.Collections.Generic;
using System.Linq;
using backend.Controller.DTOs;
using backend.Controller.Entities;
using backend.Controller.Interfaces;
using backend.Model.Interfaces;
using backend.Controller.Utilities;

namespace backend.Controller.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void ChangePass(Mail email, Password newPassword)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(email);
            Credentials creds = restaurant.Credentials;
            creds.Password = newPassword;

            _restaurantRepository.Update(restaurant);
        }
        
        public RestaurantDto GetRestaurantDtoFromMail(Mail mail)
        {
            return RestaurantDto.FromEntity(_restaurantRepository.GetByMail(mail));
        }

        public void DeleteAccount(Credentials creds)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(creds.Mail);
            _restaurantRepository.Delete(restaurant.Id);
        }

        public IEnumerable<RestaurantDto> GetAllRestaurants()
        {
            return _restaurantRepository
                    .GetAll()
                    .Select(r => RestaurantDto.FromEntity(r));
        }

        public RestaurantDto GetRestaurantById(string id)
        {
            Restaurant restaurant = _restaurantRepository.GetById(id);
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
            return restaurant != null && restaurant.Credentials.Mail.Value == creds.Mail.Value && restaurant.Credentials.Password.Value == creds.Password.Value;
        }

        public bool Register(Credentials creds, Restaurant restaurant)
        {
            if (_restaurantRepository.GetByMail(creds.Mail) == null)
            {
                string error = Validator.ValidateEmail(creds.Mail.Value) + Validator.ValidatePassword(creds.Password.Value);
                if (error == "")
                {
                    restaurant.Credentials = creds;
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
                throw new System.Exception("• There is already an account registered on this mail");
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
