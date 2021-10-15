using Domain.Entities;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        public FoodRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }
    }
}
