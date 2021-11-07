﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class FoodResponse : BaseDto
    {
        private FoodResponse(string id, string name) : base(id, name)
        {
        }

        public decimal StartingPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdRestaurant { get; set; }
        public virtual IEnumerable<TypeOfFood> TypesOfFood { get; set; }
        public DateTime StartDecreasingAt { get; set; }
        public DecreaseType DecreaseType { get; set; }
        public double IntervalTimeInMinutes { get; set; }
        public decimal AmountPerInterval { get; set; }
        public double PercentPerInterval { get; set; }

        public static FoodResponse FromEntity(Food food)
        {
            _ = food ?? throw new ArgumentNullException(nameof(food));

            return new FoodResponse(food.Id, food.Name)
            {
                StartingPrice = food.StartingPrice,
                CurrentPrice = food.CalculateCurrentPrice(),
                CreatedAt = food.CreatedAt,
                IdRestaurant = food.IdRestaurant,
                TypesOfFood = food.TypesOfFood,
                StartDecreasingAt = food.StartDecreasingAt,
                IntervalTimeInMinutes = food.IntervalTimeInMinutes,
                AmountPerInterval = food.AmountPerInterval,
                PercentPerInterval = food.PercentPerInterval,
                DecreaseType = food.DecreaseType,
            };
        }
    }
}