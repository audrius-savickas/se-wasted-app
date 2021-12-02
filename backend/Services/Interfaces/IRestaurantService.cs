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
        PagedList<RestaurantDto> GetRestaurantsNear(RestaurantParameters restaurantParametersm, Coords coords);
        RestaurantDto GetRestaurantDtoFromMail(Mail mail);
        PagedList<RestaurantDto> GetAllRestaurantsCloserThan(RestaurantParameters restaurantParameters, Coords coords, Distances distance);
        PagedList<Food> GetAllFoodFromRestaurant(string idRestaurant, FoodParameters foodParameters);
        int GetFoodCountFromRestaurant(string idRestaurant);
    }
}

