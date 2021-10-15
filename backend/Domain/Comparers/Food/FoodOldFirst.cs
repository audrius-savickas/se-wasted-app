using System.Collections.Generic;

namespace Domain.Comparers.Food
{
    public class FoodOldFirst : IComparer<Entities.Food>
    {
        public FoodOldFirst()
        { }

        public int Compare(Entities.Food x, Entities.Food y)
        {
            return -x.CreatedAt.CompareTo(y.CreatedAt);
        }
    }
}
