using System;
using System.Collections.Generic;
using System.Linq;
using console_wasted_app.Controller.Comparers.Food;

namespace console_wasted_app.Controller.Entities
{
    public static class ListOfFood
    {
        
        private static IEnumerable<Food> Order(IEnumerable<Food> list, IComparer<Food> comparer)
        {
            return list.OrderBy(food => food, comparer);
        }

        public static IEnumerable<Food> SortByPrice(this IEnumerable<Food> list)
        {
            return Order(list, new FoodCheaperFirst());
        }

        public static IEnumerable<Food> SortByNew(this IEnumerable<Food> list)
        {
            return Order(list, new FoodNewFirst());
        }

    }
}
