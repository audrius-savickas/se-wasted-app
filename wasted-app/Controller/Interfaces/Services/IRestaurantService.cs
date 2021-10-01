using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces.Services;
using System.Collections.Generic;

namespace console_wasted_app.Controller.Interfaces
{
    public interface IRestaurantService : IAuthService<Restaurant>
    {
        Restaurant GetRestaurantById(string id);
        IEnumerable<Restaurant> GetAllRestaurants();
        void UpdateRestaurant(Restaurant restaurant);
        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords);
    }
}
