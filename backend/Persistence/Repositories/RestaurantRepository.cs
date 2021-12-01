using Domain.Models;
using Domain.Models.QueryParameters;
using Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }

        public IEnumerable<Restaurant> GetAllRestaurantsCloserThan(Coords coords, Distances distance)
        {
            List<Restaurant> all = GetAll().ToList();
            List<Restaurant> restaurantsCloser = all.FindAll(
                r => r.IsCloser(coords, distance)
            );
            return restaurantsCloser;
        }

        public PagedList<Restaurant> GetAllRestaurantsCloserThan(RestaurantParameters restaurantParameters, Coords coords, Distances distance)
        {
            throw new System.NotImplementedException();
        }

        public PagedList<Restaurant> GetAllWithPaging(RestaurantParameters restaurantParameters)
        {
            throw new System.NotImplementedException();
        }

        public Restaurant GetByMail(Mail mail)
        {
            List<Restaurant> all = GetAll().ToList();
            Restaurant restaurant = all.FirstOrDefault(r => r.Credentials.Mail.Value == mail.Value);
            return restaurant;
        }

        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords)
        {
            return GetAllRestaurantsCloserThan(coords, Distances.NEAR);
        }
    }
}
