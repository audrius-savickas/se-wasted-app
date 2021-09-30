using console_wasted_app.Controller.Interfaces;
using console_wasted_app.Controller.Services;
using console_wasted_app.Model.Data;
using console_wasted_app.Model.Interfaces;
using console_wasted_app.Model.Repositories;
using System;

namespace console_wasted_app.Controller
{
    public sealed class ServicesController
    {
        private static readonly Lazy<ServicesController> _instance
            = new Lazy<ServicesController>(() => new ServicesController());

        public readonly IFoodService FoodService;
        public readonly IRestaurantService RestaurantService;
        public readonly ITypeOfFoodService TypeOfFoodService;

        public static ServicesController Instance => _instance.Value;

        private ServicesController()
        {
            // Set up database
            DBConfiguration dbConfig = DBConfiguration.Instance;

            IFoodRepository foodRepository
                = new FoodRepository(dbConfig.PathToFoodsFile);
            IRestaurantRepository restRepository
                = new RestaurantRepository(dbConfig.PathToRestaurantsFile);
            ITypeOfFoodRepository typeRepository
                = new TypeOfFoodRepository(dbConfig.PathToTypesOfFoodFile);

            // Set up services
            FoodService = new FoodService(foodRepository);
            RestaurantService = new RestaurantService(restRepository);
            TypeOfFoodService = new TypeOfFoodService(typeRepository);
        }

    }
}
