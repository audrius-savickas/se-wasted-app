using System.Collections.Generic;
using console_wasted_app.Controller.DTOs;
using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces.Services;
using System.Collections.Generic;

namespace console_wasted_app.Controller.Interfaces
{
    public interface IRestaurantService : IAuthService<Restaurant>
    {
        RestaurantDto GetRestaurantById(string id);
        IEnumerable<RestaurantDto> GetAllRestaurants();
        void UpdateRestaurant(Restaurant restaurant);
        IEnumerable<RestaurantDto> GetRestaurantsNear(Coords coords);
        RestaurantDto GetRestaurantDtoFromMail(Mail mail);
    }
}

