using backend.Controller;
using backend.Controller.Entities;
using System.Collections.Generic;
using System.Linq;

namespace wasted_app.Utilities
{
    public class FoodUtilities
    {
        private static ServicesController servicesController = ServicesController.Instance;
        public static IEnumerable<Food> GetFoodByRestaurantId(string restaurantId)
        {
            var allFood = servicesController.FoodService.GetAllFood();

            return (from food in allFood
                    where food.IdRestaurant == restaurantId
                    select food).ToList();
        }

        public static string GetFoodTypeNameById(string typeId)
        {
            var foodType = servicesController.TypeOfFoodService.GetByTypeOfFoodId(typeId);
            return foodType.Name;
        }

        public static string GetFoodTypeIdByName(string typeName)
        {
            var foodTypes = servicesController.TypeOfFoodService.GetAllTypesOfFood();
            foreach(var foodType in foodTypes)
            {
                if (foodType.Name == typeName)
                {
                    return foodType.Id;
                }
            }
            throw new System.Exception("Food type by name not found");
        }

        public static List<string> GetAllFoodTypesNames()
        {
            var foodTypes = servicesController.TypeOfFoodService.GetAllTypesOfFood();

            return (from foodType in foodTypes
                    select foodType.Name).ToList();
        }

        public static string GetFirstAvailableFoodId()
        {
            var foods = servicesController.FoodService.GetAllFood();
            var index = 0;
            foreach (var food in foods)
            {
                if (food.Id != index.ToString())
                {
                    return index.ToString();
                }
                index++;
            }
            return index.ToString();
        }
    }
}
