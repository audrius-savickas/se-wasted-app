﻿using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces;
using console_wasted_app.Model.Interfaces;
using System.Collections.Generic;

namespace console_wasted_app.Controller.Services
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
            Restaurant restaurant = GetByMail(email);
            Credentials creds = restaurant.Credentials;
            creds.Password = newPassword;

            _restaurantRepository.Update(restaurant);
        }

        public void DeleteAccount(Credentials creds)
        {
            Restaurant restaurant = GetByMail(creds.Mail);
            _restaurantRepository.Delete(restaurant.Id);
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll();
        }

        public Restaurant GetByMail(Mail email)
        {
            return _restaurantRepository.GetByMail(email);
        }

        public Restaurant GetRestaurantById(string id)
        {
            return _restaurantRepository.GetById(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords)
        {
            return _restaurantRepository.GetRestaurantsNear(coords);
        }

        public bool Login(Credentials creds)
        {
            Restaurant restaurant = GetByMail(creds.Mail);
            return restaurant != null && restaurant.Credentials.Mail.Value == creds.Mail.Value && restaurant.Credentials.Password.Value == creds.Password.Value;
        }

        public bool Register(Credentials creds, Restaurant restaurant)
        {
            restaurant.Credentials = creds;
            _restaurantRepository.Add(restaurant);
            return true;
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Update(restaurant);
        }
    }
}
