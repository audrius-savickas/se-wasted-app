using System.Collections.Generic;
using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces;
using console_wasted_app.Model.Interfaces;

namespace console_wasted_app.Controller.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;

        public FoodService (IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public void DeleteFood(string id)
        {
            _foodRepository.Delete(id);
        }

        public IEnumerable<Food> GetAllFood()
        {
            return _foodRepository.GetAll();
        }

        public Food GetFoodById(string id)
        {
            return _foodRepository.GetById(id);
        }

        public void UpdateFood(Food updatedFood)
        {
            _foodRepository.Update(updatedFood);
        }

        public Restaurant GetRestaurantOfFood(string idFood)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TypeOfFood> GetTypesOfFood(string id)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterFood(string idRestaurant, Food food)
        {
            throw new System.NotImplementedException();
        }
    }
}
