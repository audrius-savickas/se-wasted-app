using System.Collections.Generic;
using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces;
using console_wasted_app.Controller.Interfaces.Services;
using console_wasted_app.Model.Interfaces;

namespace console_wasted_app.Controller.Services
{
    public class RestaurantService : IRestaurantService, IAuthService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void ChangePass(Mail email, Password newPassword)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAccount(Credentials creds)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll();
        }

        public Restaurant GetRestaurantById(string id)
        {
            return _restaurantRepository.GetById(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords, int amount = 10)
        {
            return _restaurantRepository.GetRestaurantsNear(coords, amount);
        }

        public bool Login(Credentials creds)
        {
            throw new System.NotImplementedException();
        }

        public bool Register(Credentials creds)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Update(restaurant);
        }
    }
}
