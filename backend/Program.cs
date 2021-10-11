using backend.Controller;
using System;

namespace backend
{
    class Program
    {
        static void Main(string[] args)
        {
            var servicesController = ServicesController.Instance;

            var restaurantService = servicesController.RestaurantService;
            var restaurants = restaurantService.GetAllRestaurants();

            foreach ( var r in restaurants )
            {
                Console.WriteLine(r.Name);
            }
        }
    }
}
