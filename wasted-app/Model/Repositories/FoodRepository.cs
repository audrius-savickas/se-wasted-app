using System;
using System.Collections.Generic;
using console_wasted_app.Controller.Entities;
using console_wasted_app.Model.Interfaces;

namespace console_wasted_app.Model.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        public FoodRepository(String pathToDatabase) : base(pathToDatabase)
        {
        }

        public Restaurant GetRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TypeOfFood> GetTypesOfFood(int id)
        {
            throw new NotImplementedException();
        }
    }
}
