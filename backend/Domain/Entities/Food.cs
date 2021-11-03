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
        public double IntervalTimeInMinutes { get; set; }
        public decimal AmountPerInterval { get; set; }
        public double PercentPerInterval { get; set; }

        public Food
        (
            string id,
            string name,
            decimal startingPrice,
            string idRestaurant,
            IEnumerable<TypeOfFood> typesOfFood,
            double intervalTimeInMinutes,
            DateTime? createdAt = null,
            DateTime? startDecreasingAt = null,
            decimal? amountPerInterval = null,
            double? percentPerInterval = null
        )
            : base(id, name)
        {
            StartingPrice = startingPrice;
            CreatedAt = createdAt ?? DateTime.Now;
            IdRestaurant = idRestaurant;
            TypesOfFood = typesOfFood;
            StartDecreasingAt = startDecreasingAt ?? CreatedAt;
            IntervalTimeInMinutes = intervalTimeInMinutes;
            
            if (amountPerInterval == null && percentPerInterval == null)
            {
                throw new ArgumentException("Both decrease amount and percent cannot be null.");
            } 
            else if (amountPerInterval != null && percentPerInterval != null)
            {
                throw new ArgumentException("Cannot pick both types of price decrease at the same time.");
            }
            else if (amountPerInterval != null)
            {
                AmountPerInterval = (decimal)amountPerInterval;
                PercentPerInterval = CalculatePercentPerInterval();
            }
            else if (percentPerInterval != null)
            {
                PercentPerInterval = (double)percentPerInterval;
                AmountPerInterval = CalculateAmountPerInterval();
            }
        }

        public decimal CalculateCurrentPrice()
        {
            if (StartDecreasingAt > DateTime.Now)
            {
                return StartingPrice;
            }

            int intervalCount = (int)((DateTime.Now - StartDecreasingAt) / TimeSpan.FromMinutes(IntervalTimeInMinutes));
            decimal priceDecrease = intervalCount * AmountPerInterval;

            return StartingPrice - priceDecrease;
        }

        private decimal CalculateAmountPerInterval()
        {
            return StartingPrice * (decimal)PercentPerInterval / 100;
        }

        private double CalculatePercentPerInterval()
        {
            return decimal.ToDouble(AmountPerInterval * 100 / StartingPrice);
        }
    }
}
