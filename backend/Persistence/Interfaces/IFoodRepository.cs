using Domain.Models;
using Domain.Models.QueryParameters;

namespace Persistence.Interfaces
{
    public interface IFoodRepository : IBaseRepository<Food>
    {
        PagedList<Food> GetAllWithPaging(FoodParameters foodParameters);
    }
}
