using System.Collections.Generic;
using System.Linq;
using console_wasted_app.Controller.DTOs;
using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces;
using console_wasted_app.Model.Interfaces;

namespace console_wasted_app.Controller.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public FoodService
        (
            IFoodRepository foodRepository,
            IRestaurantRepository restaurantRepository
        )
        {
            _foodRepository = foodRepository;
            _restaurantRepository = restaurantRepository;
        }

        public void DeleteFood(string id)
        {
            _foodRepository.Delete(id);
        }

        public IEnumerable<Food> GetAllFood()
        {
            return _foodRepository.GetAll().ToList();
        }

        public Food GetFoodById(string id)
        {
            return _foodRepository.GetById(id);
        }

        public void UpdateFood(Food updatedFood)
        {
            _foodRepository.Update(updatedFood);
        }

        public RestaurantDto GetRestaurantOfFood(string idFood)
        {
            Food food = GetFoodById(idFood);
            string idRestaurant = food.IdRestaurant;
            Restaurant restaurant = _restaurantRepository.GetById(idRestaurant);

            return RestaurantDto.FromEntity(restaurant);

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
