using Domain.Models;
using System.Collections.Generic;

namespace Persistence.Interfaces
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        IEnumerable<Restaurant> GetRestaurantsNear(Coords coords);
        Restaurant GetByMail(Mail mail);
        IEnumerable<Restaurant> GetAllRestaurantsCloserThan(Coords coords, Distances distance);
    }
}
