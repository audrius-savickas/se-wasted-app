﻿using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Mappers
{
    public static class FoodMapper
    {
        public static Food ToDomain(this FoodEntity from)
        {
            DecreaseType decreaseType = string.Equals(from.DecreaseType, "AMOUNT") ? DecreaseType.AMOUNT : DecreaseType.PERCENT;
            string foodId = from.Id.ToString();
            string restaurantId = from.Id.ToString();

            return new Food(
                foodId,
                from.Name,
                from.StartingPrice,
                from.MinimumPrice,
                restaurantId,
                from.TypesOfFood.Select(x => x.ToDomain()),
                from.IntervalTimeInMinutes,
                decreaseType,
                from.ImageURL,
                from.CreatedOn,
                from.StartDecreasingAt,
                from.AmountPerInterval,
                from.PercentPerInterval);
        }

        public static FoodEntity ToEntity(this Food from)
        {
            return new FoodEntity
            {
                Id = Guid.Parse(from.Id),
                Name = from.Name,
                StartingPrice = from.StartingPrice,
                MinimumPrice = from.MinimumPrice,
                RestaurantId = Guid.Parse(from.IdRestaurant),
                TypesOfFood = (ICollection<TypeOfFoodEntity>)from.TypesOfFood.Select(x => x.ToEntity()),
                IntervalTimeInMinutes = from.IntervalTimeInMinutes,
                DecreaseType = from.DecreaseType == DecreaseType.AMOUNT ? "AMOUNT" : "PERCENT",
                ImageURL = from.ImageURL,
                StartDecreasingAt = from.StartDecreasingAt,
                AmountPerInterval = from.AmountPerInterval,
                PercentPerInterval = from.PercentPerInterval,
                Description = from.Description,
            };
        }
    }
}
