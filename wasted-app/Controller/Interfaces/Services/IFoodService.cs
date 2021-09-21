using System;
using System.Collections.Generic;
using console_wasted_app.Controller.Entities;

namespace console_wasted_app.Controller.Interfaces
{
    public interface IFoodService : IBaseService<Food>
    {
        public Restaurant GetRestaurant();
        public IEnumerable<TypeOfFood> GetTypesOfFood();
    }
}
