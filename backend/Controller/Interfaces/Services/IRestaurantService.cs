using System.Collections.Generic;
using backend.Controller.DTOs;
using backend.Controller.Entities;
using backend.Controller.Interfaces.Services;

namespace backend.Controller.Interfaces
{
    public interface IRestaurantService : IAuthService<Restaurant>
    {
        RestaurantDto GetRestaurantById(string id);
        IEnumerable<RestaurantDto> GetAllRestaurants();
        void UpdateRestaurant(Restaurant restaurant);
        IEnumerable<RestaurantDto> GetRestaurantsNear(Coords coords);
        RestaurantDto GetRestaurantDtoFromMail(Mail mail);
        IEnumerable<RestaurantDto> GetAllRestaurantsCloserThan(Coords coords, Distances distance);
    }
}

