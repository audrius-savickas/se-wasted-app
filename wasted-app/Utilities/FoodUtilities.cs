using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
using System.Collections.Generic;

namespace wasted_app.Utilities
{
    internal class FoodUtilities
    {
        public static IEnumerable<Food> GetFoodByRestaurantId(string restaurantId)
        {
            var allFood = ServicesController.Instance.FoodService.GetAllFood();
            var restaurantFood = new List<Food>();

            foreach (var food in allFood)
            {
                if (food.IdRestaurant == restaurantId)
                {
                    restaurantFood.Add(food);
                }
            }
            return restaurantFood;
        }

        public static string GetFoodTypeName(string typeId)
        {
            var foodType = ServicesController.Instance.TypeOfFoodService.GetByTypeOfFoodId(typeId);
            return foodType.Name;
        }
    }
}
