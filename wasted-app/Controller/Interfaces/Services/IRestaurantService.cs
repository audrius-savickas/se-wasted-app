using System;
using System.Collections.Generic;
using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces.Services;

namespace console_wasted_app.Controller.Interfaces
{
    public interface IRestaurantService
    {
        Restaurant GetRestaurantById(string id);
        IEnumerable<Restaurant> GetAllRestaurants();
        void UpdateRestaurant(Restaurant restaurant);
        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords, int amount = 10);
    }
}
