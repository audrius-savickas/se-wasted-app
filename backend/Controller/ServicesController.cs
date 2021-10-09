using backend.Controller.Interfaces;
using backend.Controller.Services;
using backend.Model.Data;
using backend.Model.Interfaces;
using backend.Model.Repositories;
using System;

namespace backend.Controller
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
            FoodService = new FoodService(foodRepository, restRepository);
            RestaurantService = new RestaurantService(restRepository);
            TypeOfFoodService = new TypeOfFoodService(typeRepository);
        }

    }
}
