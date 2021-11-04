using Domain.Entities;
using System;

namespace Contracts.DTOs
{
    public class RestaurantDto : BaseDto
    {
        public Coords Coords { get; set; }

        public string Address { get; set; }

        public string ImageURL { get; set; }

        private const string DEFAULT_IMAGE_URL = "https://www.google.lt/url?sa=i&url=https%3A%2F%2Fcommons.wikimedia.org%2Fwiki%2FFile%3ANo-Image-Placeholder.svg&psig=AOvVaw1uR0Zny7PYJn8sTl750JMW&ust=1636147269746000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCKCc6rvS__MCFQAAAAAdAAAAABAD";

        public RestaurantDto
        (
            string id,
            string name,
            string address,
            Coords coords,
            string imageURL = DEFAULT_IMAGE_URL
        )
            : base(id, name)
        {
            Address = address;
            Coords = coords ?? throw new ArgumentNullException(nameof(coords));
            ImageURL = imageURL;
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
