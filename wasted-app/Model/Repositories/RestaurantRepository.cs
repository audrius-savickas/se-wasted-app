using System;
using System.Collections.Generic;
using System.Linq;
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

        public Restaurant GetByMail(Mail mail)
        {
            List<Restaurant> all = GetAll().ToList();
            Restaurant restaurant = all.FirstOrDefault(r => r.Credentials.Mail.Value == mail.Value);
            return restaurant;
        }

        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords)
        {
            List<Restaurant> all = GetAll().ToList();
            List<Restaurant> restaurantsNear = all.FindAll(
                r => r.IsNear(coords)
            );
            return restaurantsNear;
        }
    }
}
