using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Food : BaseEntity
    {
        public decimal StartingPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdRestaurant { get; set; }
        public virtual IEnumerable<TypeOfFood> TypesOfFood { get; set; }
        public DateTime StartDecreasingAt { get; set; }
        public TimeSpan IntervalTime { get; set; }
        public decimal AmountPerInterval { get; set; }
        public double PercentPerInterval { get; set; }


        public Food() : base() { }

        public Food
        (
            string id,
            string name,
            decimal price,
            string idRestaurant,
            IEnumerable<TypeOfFood> typesOfFood,
            TimeSpan intervalTime,
            DateTime? createdAt = null,
            DateTime? startDecreasingAt = null,
            decimal? amountPerInterval = null,
            double? percentPerInterval = null
        )
            : base(id, name)
        {
            StartingPrice = price;
            CreatedAt = createdAt ?? DateTime.Now;
            IdRestaurant = idRestaurant;
            TypesOfFood = typesOfFood;
            StartDecreasingAt = startDecreasingAt ?? CreatedAt;
            IntervalTime = intervalTime;
            
            if (amountPerInterval == null && percentPerInterval == null)
            {
                throw new ArgumentException("Both decrease amount and percent cannot be null.");
            }

            AmountPerInterval = amountPerInterval ?? CalculateAmountPerInterval();
            PercentPerInterval = percentPerInterval ?? CalculatePercentPerInterval();
        }

        private decimal CalculateAmountPerInterval()
        {
            return 0; // TODO
        }

        private double CalculatePercentPerInterval()
        {
            return 0; // TODO
        }

        public decimal CalculateCurrentPrice()
        {
            return StartingPrice; // TODO
        }
    }
}
