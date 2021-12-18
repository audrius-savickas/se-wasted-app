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
        RestaurantDto GetRestaurantById(string id, Coords coords = null);
        PagedList<RestaurantDto> GetAllRestaurants(RestaurantParameters restaurantParameters);
        void UpdateRestaurant(Restaurant restaurant);
        PagedList<RestaurantDto> GetRestaurantsNear(RestaurantParameters restaurantParametersm);
        RestaurantDto GetRestaurantDtoFromMail(Mail mail, Coords coords = null);
        PagedList<RestaurantDto> GetAllRestaurantsCloserThan(RestaurantParameters restaurantParameters, Distances distance);
        PagedList<Food> GetAllFoodFromRestaurant(string idRestaurant, FoodParameters foodParameters, bool reserved);
        PagedList<Food> GetAllReservedFoodFromRestaurant(string idRestaurant, FoodParameters foodParameters);
        int GetFoodCountFromRestaurant(string idRestaurant);
    }
}

