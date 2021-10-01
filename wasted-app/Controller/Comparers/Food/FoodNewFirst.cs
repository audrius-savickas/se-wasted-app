﻿using System.Collections.Generic;

namespace console_wasted_app.Controller.Comparers.Food
{
    public class FoodNewFirst : IComparer<Entities.Food>
    {
        public FoodNewFirst()
        { }

        public int Compare(Entities.Food x, Entities.Food y)
        {
            return x.CreatedAt.CompareTo(y.CreatedAt);
        }
    }
}
