using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public static class ListOfFood
    {   
        public static IEnumerable<Food> SortByPrice(this IEnumerable<Food> list)
        {
            return list.OrderBy(x => x.Price);
        }

        public static IEnumerable<Food> SortByNew(this IEnumerable<Food> list)
        {
            return list.OrderBy(x => x.CreatedAt);
        }

        public static IEnumerable<Food> SortByPriceReverse(this IEnumerable<Food> list)
        {
            return list.OrderByDescending(x => x.Price);
        }

        public static IEnumerable<Food> SortByNewReverse(this IEnumerable<Food> list)
        {
            return list.OrderByDescending(x => x.CreatedAt);
        }

    }
}
