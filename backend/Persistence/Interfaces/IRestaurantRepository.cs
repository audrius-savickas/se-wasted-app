using Domain.Models;
using Domain.Models.QueryParameters;
using System.Collections.Generic;

namespace Persistence.Interfaces
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        PagedList<Restaurant> GetAllWithPaging(RestaurantParameters restaurantParameters);
        IEnumerable<Restaurant> GetRestaurantsNear(Coords coords);
        Restaurant GetByMail(Mail mail);
        IEnumerable<Restaurant> GetAllRestaurantsCloserThan(Coords coords, Distances distance);
    }
}
