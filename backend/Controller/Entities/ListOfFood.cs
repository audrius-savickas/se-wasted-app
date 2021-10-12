using System;
using System.Collections.Generic;
using System.Linq;
using backend.Controller.Comparers.Food;

namespace backend.Controller.Entities
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

        public static IEnumerable<Food> SortByPriceReverse(this IEnumerable<Food> list)
        {
            return Order(list, new FoodExpensiveFirst());
        }

        public static IEnumerable<Food> SortByNewReverse(this IEnumerable<Food> list)
        {
            return Order(list, new FoodOldFirst());
        }

    }
}
