using backend.Controller.Entities;
using backend.Model.Interfaces;

namespace backend.Model.Repositories
{
    public class TypeOfFoodRepository : BaseRepository<TypeOfFood>, ITypeOfFoodRepository
    {
        public TypeOfFoodRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }
    }
}
