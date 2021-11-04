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
        public DecreaseType DecreaseType { get; set; }
        public double IntervalTimeInMinutes { get; set; }
        public decimal AmountPerInterval { get; set; }
        public double PercentPerInterval { get; set; }

        public Food() : base() { }

        public Food
        (
            string id,
            string name,
            decimal startingPrice,
            string idRestaurant,
            IEnumerable<TypeOfFood> typesOfFood,
            double intervalTimeInMinutes,
            DecreaseType decreaseType,
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
            DecreaseType = decreaseType;

            if (decreaseType != DecreaseType.AMOUNT && decreaseType != DecreaseType.PERCENT)
            {
                throw new ArgumentException("Invalid price decrease type.");
            }
            else if (decreaseType == DecreaseType.AMOUNT && amountPerInterval == null)
            {
                throw new ArgumentNullException(nameof(amountPerInterval));
            }
            else if (decreaseType == DecreaseType.AMOUNT && percentPerInterval == null)
            {
                throw new ArgumentNullException(nameof(percentPerInterval));
            }
            else if (decreaseType == DecreaseType.AMOUNT)
            {
                AmountPerInterval = (decimal)amountPerInterval;
                PercentPerInterval = CalculatePercentPerInterval();
            }
            else if (decreaseType == DecreaseType.AMOUNT)
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

            decimal priceDecrease = 0;
            if (DecreaseType == DecreaseType.AMOUNT)
            {
                priceDecrease = intervalCount * AmountPerInterval;
            }
            else if (DecreaseType == DecreaseType.AMOUNT)
            {
                priceDecrease = StartingPrice * (decimal)(PercentPerInterval / 100) * intervalCount;
            }

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

    public enum DecreaseType
    {
        AMOUNT,
        PERCENT
    }
}
