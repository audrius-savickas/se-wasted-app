using System;
using console_wasted_app.Controller.Interfaces;
using console_wasted_app.Controller.Services;
using console_wasted_app.Model.Data;
using console_wasted_app.Model.Interfaces;
using console_wasted_app.Model.Repositories;

namespace console_wasted_app.Controller
{
    public sealed class ServicesController
    {
        private static readonly Lazy<ServicesController> _instance
            = new Lazy<ServicesController>(() => new ServicesController());

        public readonly IFoodService FoodService;
        public readonly IRestaurantService RestaurantService;
        public readonly ITypeOfFoodService TypeOfFoodService;

        public static ServicesController Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public ServicesController()
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
            this.FoodService = new FoodService(foodRepository);
            this.RestaurantService = new RestaurantService(restRepository);
            this.TypeOfFoodService = new TypeOfFoodService(typeRepository);
        }

    }
}
