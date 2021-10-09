using backend.Controller;
using backend.Controller.Entities;
using backend.Controller.Interfaces;
using backend.Model.Interfaces;

namespace backend.Model.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        public FoodRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }

        public TypeOfFood GetTypeOfFood(string id)
        {
            Food food = GetById(id);
            string idTypeOfFood = food.IdTypeOfFood;

            ServicesController servicesController = ServicesController.Instance;
            ITypeOfFoodService typeOfFoodService = servicesController.TypeOfFoodService;

            return typeOfFoodService.GetByTypeOfFoodId(idTypeOfFood);
        }
    }
}
