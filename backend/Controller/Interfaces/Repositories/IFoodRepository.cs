using backend.Controller.Entities;
using backend.Controller.Interfaces;

namespace backend.Model.Interfaces
{
    public interface IFoodRepository : IBaseRepository<Food>
    {
        public TypeOfFood GetTypeOfFood(string id);
    }
}
