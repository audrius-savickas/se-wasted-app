using Domain.Entities;
using System;

namespace Contracts.DTOs
{
    public class RestaurantDto : BaseDto
    {
        public Coords Coords { get; set; }

        public string Address { get; set; }

        public string ImageURL { get; set; }

        private const string DEFAULT_IMAGE_URL = "todo";

        public RestaurantDto
        (
            string id,
            string name,
            string address,
            Coords coords,
            string? imageURL = null
        )
            : base(id, name)
        {
            Address = address;
            Coords = coords ?? throw new ArgumentNullException(nameof(coords));
            ImageURL = imageURL ?? DEFAULT_IMAGE_URL;
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
                restaurant.ImageURL
            );
        }
    }
}
