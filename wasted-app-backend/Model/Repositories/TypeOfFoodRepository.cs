using console_wasted_app.Controller.Entities;
using console_wasted_app.Model.Interfaces;

namespace console_wasted_app.Model.Repositories
{
    public class TypeOfFoodRepository : BaseRepository<TypeOfFood>, ITypeOfFoodRepository
    {
        public TypeOfFoodRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }
    }
}
