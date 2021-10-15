using System.Collections.Generic;

namespace Domain.Comparers.Food
{
    public class FoodCheaperFirst : IComparer<Entities.Food>
    {
        public FoodCheaperFirst()
        { }

        public int Compare(Entities.Food x, Entities.Food y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }
}
