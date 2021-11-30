using Contracts.DTOs;
using Domain.Models;
using Domain.Models.QueryParameters;
using Services.Services;
using System;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IRestaurantService : IAuthService<RestaurantRegisterRequest>
    {
        event EventHandler<RestaurantEventArgs> RestaurantRegistered;
        RestaurantDto GetRestaurantById(string id);
        PagedList<RestaurantDto> GetAllRestaurants(RestaurantParameters restaurantParameters);
        void UpdateRestaurant(Restaurant restaurant);
        IEnumerable<RestaurantDto> GetRestaurantsNear(Coords coords);
        RestaurantDto GetRestaurantDtoFromMail(Mail mail);
        IEnumerable<RestaurantDto> GetAllRestaurantsCloserThan(Coords coords, Distances distance);
        IEnumerable<Food> GetAllFoodFromRestaurant(string idRestaurant);
        int GetFoodCountFromRestaurant(string idRestaurant);
    }
}

