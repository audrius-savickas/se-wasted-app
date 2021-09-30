using console_wasted_app.Controller.Entities;
using console_wasted_app.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace console_wasted_app.Model.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }

        public Restaurant GetByMail(Mail mail)
        {
            var all = GetAll().ToList();
            var restaurant = all.FirstOrDefault(r => r.Credentials.Mail.Value == mail.Value);
            return restaurant;
        }

        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords)
        {
            var all = GetAll().ToList();
            var restaurantsNear = all.FindAll(
                r => r.IsNear(coords)
            );
            return restaurantsNear;
        }
    }
}
