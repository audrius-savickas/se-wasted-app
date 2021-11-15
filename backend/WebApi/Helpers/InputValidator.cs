using Domain.Entities;
using System;

namespace WebApi.Helpers
{
    public class InputValidator
    {
        private enum RestaurantSortOrders
        {
            dist,
            dist_desc,
            name,
            name_desc,
            foodCount,
            foodCount_desc
        }

        private enum FoodSortOrders
        {
            name,
            name_desc,
            price,
            price_desc,
            time,
            time_desc
        }

        public static bool ValidateRestaurantSortOrder(string sortOrder, Coords userCoordinates = null)
        {
            if (userCoordinates == null && (sortOrder == "dist" || sortOrder == "dist_desc"))
            {
                throw new ArgumentException("Invalid user coordinates.");
            }

            if (!Enum.IsDefined(typeof(RestaurantSortOrders), sortOrder))
            {
                throw new ArgumentException("Invalid sort order");
            }

            return true;
        }

        public static bool ValidateFoodSortOrder(string sortOrder)
        {
            if (!Enum.IsDefined(typeof(FoodSortOrders), sortOrder))
            {
                throw new ArgumentException("Invalid sort order");
            }

            return true;
        }





    }
}
