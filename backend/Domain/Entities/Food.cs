using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Food : BaseEntity
    {
        public decimal StartingPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdRestaurant { get; set; }
        public string ImageURL { get; set; }
        public virtual IEnumerable<TypeOfFood> TypesOfFood { get; set; }
        public DateTime StartDecreasingAt { get; set; }
        public DecreaseType DecreaseType { get; set; }
        public double IntervalTimeInMinutes { get; set; }
        public decimal AmountPerInterval { get; set; }
        public double PercentPerInterval { get; set; }

        private const string DEFAULT_IMAGE_URL = "https://genesisairway.com/wp-content/uploads/2019/05/no-image.jpg";

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
            string imageURL = DEFAULT_IMAGE_URL,
            DateTime? createdAt = null,
            DateTime? startDecreasingAt = null,
            decimal? amountPerInterval = null,
            double? percentPerInterval = null
        )
            : base(id, name)
        {
            StartingPrice = startingPrice;
            CreatedAt = createdAt ?? DateTime.Now;
            ImageURL = imageURL;
            IdRestaurant = idRestaurant;
            TypesOfFood = typesOfFood;
            StartDecreasingAt = startDecreasingAt ?? CreatedAt;
            IntervalTimeInMinutes = intervalTimeInMinutes;
            DecreaseType = decreaseType;

            ValidatePriceDecrease(decreaseType, percentPerInterval, amountPerInterval);

            switch (decreaseType)
            {
                case DecreaseType.AMOUNT:
                    AmountPerInterval = (decimal)amountPerInterval;
                    PercentPerInterval = CalculatePercentPerInterval();
                    break;
                case DecreaseType.PERCENT:
                    PercentPerInterval = (double)percentPerInterval;
                    AmountPerInterval = CalculateAmountPerInterval();
                    break;
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
            else if (DecreaseType == DecreaseType.PERCENT)
            {
                priceDecrease = StartingPrice * (decimal)(PercentPerInterval / 100) * intervalCount;
            }

            return StartingPrice - priceDecrease;
        }

        private void ValidatePriceDecrease(DecreaseType type, double? percent, decimal? amount)
        {
            if (type != DecreaseType.AMOUNT && type != DecreaseType.PERCENT)
            {
                throw new ArgumentException("Invalid price decrease type.");
            }
            else if (type == DecreaseType.AMOUNT && amount == null)
            {
                throw new ArgumentNullException(nameof(amount));
            }
            else if (type == DecreaseType.PERCENT && percent == null)
            {
                throw new ArgumentNullException(nameof(percent));
            }
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
