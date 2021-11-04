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
        public string DecreaseType { get; set; }
        public double IntervalTimeInMinutes { get; set; }
        public decimal AmountPerInterval { get; set; }
        public double PercentPerInterval { get; set; }

        private const string AMOUNT_STR = "AMOUNT";
        private const string PERCENT_STR = "PERCENT";

        public Food() : base() { }

        public Food
        (
            string id,
            string name,
            decimal startingPrice,
            string idRestaurant,
            IEnumerable<TypeOfFood> typesOfFood,
            double intervalTimeInMinutes,
            string decreaseType,
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

            if (decreaseType != AMOUNT_STR && decreaseType != PERCENT_STR)
            {
                throw new ArgumentException("Invalid price decrease type.");
            }
            else if (decreaseType == AMOUNT_STR && amountPerInterval == null)
            {
                throw new ArgumentNullException(nameof(amountPerInterval));
            }
            else if (decreaseType == PERCENT_STR && percentPerInterval == null)
            {
                throw new ArgumentNullException(nameof(percentPerInterval));
            }
            else if (decreaseType == AMOUNT_STR)
            {
                AmountPerInterval = (decimal)amountPerInterval;
                PercentPerInterval = CalculatePercentPerInterval();
            }
            else if (decreaseType == PERCENT_STR)
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
            if (DecreaseType == AMOUNT_STR)
            {
                priceDecrease = intervalCount * AmountPerInterval;
            }
            else if (DecreaseType == PERCENT_STR)
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
}
