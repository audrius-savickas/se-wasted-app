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
        IQueryable<Restaurant> GetAllRestaurantsCloserThan<TKey>(Coords coords, Distances distance, Expression<Func<Restaurant, TKey>> keySelector);
        IQueryable<Restaurant> GetRestaurantsNear<TKey>(Coords coords, Expression<Func<Restaurant, TKey>> keySelector);
        Restaurant GetByMail(Mail mail);
    }
}
