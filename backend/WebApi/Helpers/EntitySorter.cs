using Contracts.DTOs;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Helpers
{
    public class EntitySorter
    {
        public static IEnumerable<FoodResponse> SortFoods(IEnumerable<FoodResponse> foods, string sortOrder)
        {
            switch (sortOrder)
            {
                case "name":
                    foods = foods.OrderBy(f => f.Name).ToList();
                    break;
                case "name_desc":
                    foods = foods.OrderByDescending(f => f.Name).ToList();
                    break;
                case "price":
                    foods = foods.OrderBy(f => f.CurrentPrice).ToList();
                    break;
                case "price_desc":
                    foods = foods.OrderByDescending(f => f.CurrentPrice).ToList();
                    break;
                case "time":
                    foods = foods.OrderBy(f => (DateTime.Now - f.CreatedAt)).ToList();
                    break;
                case "time_desc":
                    foods = foods.OrderByDescending(f => (DateTime.Now - f.CreatedAt)).ToList();
                    break;
                default:
                    break;
            }

            return foods;
        }

        public static IEnumerable<RestaurantDto> SortRestaurants(IEnumerable<RestaurantDto> restaurants, string sortOrder, Coords userCoordinates)
        {
            switch (sortOrder)
            {
                case "name":
                    restaurants = restaurants.OrderBy(r => r.Name);
                    break;
                case "name_desc":
                    restaurants = restaurants.OrderByDescending(r => r.Name);
                    break;
                case "dist":
                    restaurants = restaurants.OrderBy(r => CoordsHelper.HaversineDistanceKM(userCoordinates, r.Coords));
                    break;
                case "dist_desc":
                    restaurants = restaurants.OrderByDescending(r => CoordsHelper.HaversineDistanceKM(userCoordinates, r.Coords));
                    break;
                default:
                    break;
            }

            return restaurants;
        }

    }
}
