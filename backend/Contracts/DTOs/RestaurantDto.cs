using Domain.Entities;
using System;

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
            if(coords == null)
            {
                throw new Exception("Invalid coordinates.");
            }
            else
            {
                Coords = coords;
            }
           
        }

        public static RestaurantDto FromEntity(Restaurant restaurant)
        {
            if(restaurant == null)
            {
                throw new Exception("Invalid restaurant.");
            }
            else
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
}
