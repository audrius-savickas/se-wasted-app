using Domain.Models;
using Domain.Models.QueryParameters;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        public FoodRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }

        public PagedList<Food> GetAllWithPaging(FoodParameters foodParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
