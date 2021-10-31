using System.Collections.Generic;
using Domain.Entities;
using Contracts.DTOs;

namespace Services.Interfaces
{
    public interface IRestaurantService : IAuthService<RestaurantRegisterRequest>
    {
        RestaurantDto GetRestaurantById(string id);
        IEnumerable<RestaurantDto> GetAllRestaurants();
        void UpdateRestaurant(Restaurant restaurant);
        IEnumerable<RestaurantDto> GetRestaurantsNear(Coords coords);
        RestaurantDto GetRestaurantDtoFromMail(Mail mail);
        IEnumerable<RestaurantDto> GetAllRestaurantsCloserThan(Coords coords, Distances distance);
        IEnumerable<Food> GetAllFoodFromRestaurant(string idRestaurant);
    }
}

