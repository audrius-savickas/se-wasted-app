using System.Collections.Generic;

namespace Domain.Comparers.Food
{
    public class FoodExpensiveFirst : IComparer<Entities.Food>
    {
        public FoodExpensiveFirst()
        { }

        public int Compare(Entities.Food x, Entities.Food y)
        {
            return (int)-(x.Price - y.Price);
        }
    }
}
