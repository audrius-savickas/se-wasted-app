﻿using Contracts.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Utils
{
    public static class SortingExtensions
    {
        public static IEnumerable<Food> SortFood(this IEnumerable<Food> foods, string order)
        {
            switch (order)
            {
                case "name":
                    foods = foods.OrderBy(f => f.Name);
                    break;
                case "name_desc":
                    foods = foods.OrderByDescending(f => f.Name);
                    break;
                case "price":
                    foods = foods.OrderBy(f => f.CalculateCurrentPrice());
                    break;
                case "price_desc":
                    foods = foods.OrderByDescending(f => f.CalculateCurrentPrice());
                    break;
                case "time":
                    foods = foods.OrderBy(f => (DateTime.Now - f.CreatedAt));
                    break;
                case "time_desc":
                    foods = foods.OrderByDescending(f => (DateTime.Now - f.CreatedAt));
                    break;
                default:
                    break;
            }

            return foods;
        }

        public static IEnumerable<Restaurant> SortRestaurants(this IEnumerable<Restaurant> restaurants, string order)
        {
            switch (order)
            {
                case "name":
                    restaurants = restaurants.OrderBy(r => r.Name);
                    break;
                case "name_desc":
                    restaurants = restaurants.OrderByDescending(r => r.Name);
                    break;
                case "dist":
                    restaurants = restaurants.OrderBy(r => RestaurantDto.FromEntity(r).DistanceToUser);
                    break;
                case "dist_desc":
                    restaurants = restaurants.OrderByDescending(r => RestaurantDto.FromEntity(r).DistanceToUser);
                    break;
                case "foodCount":
                    restaurants = restaurants.OrderBy(r => RestaurantDto.FromEntity(r).FoodCount);
                    break;
                case "foodCount_desc":
                    restaurants = restaurants.OrderByDescending(r => RestaurantDto.FromEntity(r).FoodCount);
                    break;
                default:
                    break;
            }

            return restaurants;
        }
    }
}