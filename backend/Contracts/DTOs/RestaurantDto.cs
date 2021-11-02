using Domain.Entities;

namespace Contracts.DTOs
{
    public class RestaurantDto : BaseDto
    {
        public Coords Coords { get; set; }
        public string Address { get; set; }
        public Image Image { get; set; }

        public RestaurantDto
        (
            string id,
            string name,
            string address,
            Coords coords,
            Image? image = null
        )
            : base(id, name)
        {
            Address = address;
            Coords = coords;
            Image = image;
        }

        public static RestaurantDto FromEntity(Restaurant restaurant, Image? image = null)
        {
            return new RestaurantDto
            (
                restaurant.Id,
                restaurant.Name,
                restaurant.Address,
                restaurant.Coords,
                image
            );
        }

    }
}
