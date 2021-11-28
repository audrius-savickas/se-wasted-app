using Domain.Models;
using Persistence.File.Interfaces;

namespace Persistence.File.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        public FoodRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }
    }
}
