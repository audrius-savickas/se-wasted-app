using Domain.Models;
using Domain.Models.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.Interfaces
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        IQueryable<Restaurant> GetAllRestaurantsCloserThan(Coords coords, Distances distance);
        IQueryable<Restaurant> GetRestaurantsNear(Coords coords);
        Restaurant GetByMail(Mail mail);
    }
}
