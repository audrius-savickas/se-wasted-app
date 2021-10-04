using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces;
using console_wasted_app.Model.Interfaces;
using System.Collections.Generic;

namespace console_wasted_app.Controller.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;

        public FoodService(IFoodRepository foodRepository)
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
            return _foodRepository.GetRestaurant(idFood);
        }

        public TypeOfFood GetTypeOfFood(string id)
        {
            return _foodRepository.GetTypeOfFood(id);
        }

        public void RegisterFood(Food food)
        {
            _foodRepository.Add(food);
        }
    }
}
