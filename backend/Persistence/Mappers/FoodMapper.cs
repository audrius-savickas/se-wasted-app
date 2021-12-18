using Contracts.DTOs;
using Domain.Entities;
using Domain.Models;
using Persistence.Mappers;
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
            string restaurantId = from.RestaurantId.ToString();
            var reservations = from.Reservations.Select(x => x.ToDomain());

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
                from.PercentPerInterval,
                from.Description,
                reservations);
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
                TypesOfFood = from.TypesOfFood.Select(x => x.ToEntity()).ToList(),
                IntervalTimeInMinutes = from.IntervalTimeInMinutes,
                DecreaseType = from.DecreaseType == DecreaseType.AMOUNT ? "AMOUNT" : "PERCENT",
                ImageURL = from.ImageURL,
                CreatedOn = from.CreatedAt,
                StartDecreasingAt = from.StartDecreasingAt,
                AmountPerInterval = from.AmountPerInterval,
                PercentPerInterval = from.PercentPerInterval,
                Description = from.Description,
            };
        }

        public static FoodResponse ToFoodResponse(this Food from)
        {
            _ = from ?? throw new ArgumentNullException(nameof(from));

            return new FoodResponse(from.Id, from.Name)
            {
                StartingPrice = from.StartingPrice,
                MinimumPrice = from.MinimumPrice,
                CurrentPrice = from.CalculateCurrentPrice(),
                CreatedAt = from.CreatedAt,
                IdRestaurant = from.IdRestaurant,
                TypesOfFood = from.TypesOfFood,
                StartDecreasingAt = from.StartDecreasingAt,
                IntervalTimeInMinutes = from.IntervalTimeInMinutes,
                AmountPerInterval = from.AmountPerInterval,
                PercentPerInterval = from.PercentPerInterval,
                DecreaseType = from.DecreaseType,
                Description = from.Description,
                ImageURL = from.ImageURL,
                Reservation = from.Reservation?.ToDTO(),
            };
        }
    }
}
