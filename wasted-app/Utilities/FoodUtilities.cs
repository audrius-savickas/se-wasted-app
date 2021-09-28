using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wasted_app.Utilities
{
    class FoodUtilities
    {
        public static IEnumerable<Food> GetFoodByRestaurantId (string restaurantId)
        {
            IEnumerable <Food> allFood = ServicesController.Instance.FoodService.GetAllFood();
            List <Food> restaurantFood = new();

            foreach(var food in allFood)
            {
                if (food.IdRestaurant == restaurantId)
                {
                    restaurantFood.Add(food);
                }
            }
            return restaurantFood;
        }

        public static String GetFoodTypeName (string typeId)
        {
            TypeOfFood foodType = ServicesController.Instance.TypeOfFoodService.GetByTypeOfFoodId(typeId);
            return foodType.Name;
        }
    }
}
