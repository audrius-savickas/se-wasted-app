using System.Collections.Generic;

namespace console_wasted_app.Controller.Comparers.Food
{
    public class FoodCheaperFirst : IComparer<Entities.Food>
    {
        public FoodCheaperFirst()
        {}

        public int Compare(Entities.Food x, Entities.Food y)
        {
            return (int)(x.Price - y.Price);
        }
    }
}
