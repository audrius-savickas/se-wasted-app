using Domain.Models;
using Domain.Models.QueryParameters;
using System.Linq;

namespace Persistence.Interfaces
{
    public interface IFoodRepository : IBaseRepository<Food>
    {
        IQueryable<Food> GetFoodFromRestaurant(string idRestaurant);
    }
}
