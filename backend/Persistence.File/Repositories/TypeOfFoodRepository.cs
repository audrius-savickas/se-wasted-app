using Domain.Models;
using Persistence.File.Interfaces;

namespace Persistence.File.Repositories
{
    public class TypeOfFoodRepository : BaseRepository<TypeOfFood>, ITypeOfFoodRepository
    {
        public TypeOfFoodRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }
    }
}
