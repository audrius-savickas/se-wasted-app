using Domain.Entities;

namespace Contracts.DTOs
{
    public class RestaurantDto : BaseDto
    {
        public Coords Coords { get; set; }

        public string Address { get; set; }

        public RestaurantDto
        (
            string id,
            string name,
            string address,
            Coords coords
        )
            : base(id, name)
        {
            Address = address;
            Coords = coords;
        }

        public static RestaurantDto FromEntity(Restaurant restaurant)
        {
            return new RestaurantDto
            (
                restaurant.Id,
                restaurant.Name,
                restaurant.Address,
                restaurant.Coords
            );
        }
    }
}
