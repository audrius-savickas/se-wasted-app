using backend.Controller.Entities;
using backend.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace backend.Model.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(string pathToDatabase) : base(pathToDatabase)
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
