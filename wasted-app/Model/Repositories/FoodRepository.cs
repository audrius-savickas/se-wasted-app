using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
using console_wasted_app.Model.Interfaces;

namespace console_wasted_app.Model.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        public FoodRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }

        public Restaurant GetRestaurant(string id)
        {
            var food = GetById(id);
            var idRestaurant = food.IdRestaurant;

            var servicesController = ServicesController.Instance;
            var restaurantService = servicesController.RestaurantService;

            return restaurantService.GetRestaurantById(idRestaurant);
        }

        public TypeOfFood GetTypeOfFood(string id)
        {
            var food = GetById(id);
            var idTypeOfFood = food.IdTypeOfFood;

            var servicesController = ServicesController.Instance;
            var typeOfFoodService = servicesController.TypeOfFoodService;

            return typeOfFoodService.GetByTypeOfFoodId(idTypeOfFood);
        }
    }
}
