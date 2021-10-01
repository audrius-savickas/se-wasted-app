using System;
using System.Collections.Generic;
using System.Linq;
using console_wasted_app.Controller.Comparers.Food;

namespace console_wasted_app.Controller.Entities
{
    public static class ListOfFood
    {

        private static IEnumerable<Food> GetCloned(IEnumerable<Food> list)
        {
            List<Food> clonedList = new List<Food>();

            list.ToList().ForEach((food) =>
            {
                clonedList.Add((Food)food.Clone());
            });

            return clonedList;
        }

        public static IEnumerable<Food> SortByPrice(this IEnumerable<Food> list)
        {
            IEnumerable<Food> foods = GetCloned(list);

            IComparer<Food> comparer = new FoodCheaperFirst();
            foods.ToList().Sort(comparer);

            return foods;
        }

        public static IEnumerable<Food> SortByNew(this IEnumerable<Food> list)
        {
            IEnumerable<Food> foods = GetCloned(list);

            IComparer<Food> comparer = new FoodNewFirst();
            foods.ToList().Sort(comparer);

            return foods;
        }

    }
}
