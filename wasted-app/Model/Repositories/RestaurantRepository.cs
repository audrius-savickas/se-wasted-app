using System;
using System.Collections.Generic;
using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces;
using console_wasted_app.Model.Interfaces;

namespace console_wasted_app.Model.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(String pathToDatabase) : base(pathToDatabase)
        {
        }

        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords, int amount = 10)
        {
            throw new NotImplementedException();
        }
    }
}
