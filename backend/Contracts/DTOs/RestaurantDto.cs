using Domain.Entities;
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
            string description = "",
            string imageURL = ""
        )
            : base(id, name)
        {
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

        public static RestaurantDto FromEntity(Restaurant restaurant)
        {
            _ = restaurant ?? throw new ArgumentNullException(nameof(restaurant));

            return new RestaurantDto
            (
                restaurant.Id,
                restaurant.Name,
                restaurant.Address,
                restaurant.Coords,
                restaurant.Description,
                restaurant.ImageURL
            );
        }
    }
}
