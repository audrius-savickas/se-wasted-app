using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces;

namespace console_wasted_app.Model.Interfaces
{
    public interface IFoodRepository : IBaseRepository<Food>
    {
        public Restaurant GetRestaurant(string id);
        public TypeOfFood GetTypeOfFood(string id);
    }
}
