using backend.Controller.Entities;
using backend.Controller.Interfaces;
using System.Collections.Generic;

namespace backend.Model.Interfaces
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords);
        public Restaurant GetByMail(Mail mail);
        public IEnumerable<Restaurant> GetAllRestaurantsCloserThan(Coords coords, Distances distance);
    }
}
