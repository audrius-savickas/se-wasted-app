using Domain.Models;
using System;

namespace Contracts.DTOs
{
    public class RestaurantDto : BaseDto
    {
        public Coords Coords { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        private const string DEFAULT_IMAGE_URL = "https://genesisairway.com/wp-content/uploads/2019/05/no-image.jpg";

        public double DistanceToUser { get; set; }

        public int FoodCount { get; set; }

        public RestaurantDto
        (
            string id,
            string name,
            string address,
            Coords coords,
            double distanceToUser,
            int foodCount,
            string description = "",
            string imageURL = ""
        )
            : base(id, name)
        {
            FoodCount = foodCount;
            DistanceToUser = distanceToUser;
            Description = description;
            Address = address;
            Coords = coords ?? throw new ArgumentNullException(nameof(coords));
            if (imageURL == "")
            {
                ImageURL = DEFAULT_IMAGE_URL;
            }
            else
            {
                ImageURL = imageURL;
            }
        }
    }
}
