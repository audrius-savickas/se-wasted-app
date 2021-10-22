using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Persistence.Interfaces;
using Contracts.DTOs;
using Services.Interfaces;

namespace Services.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ITypeOfFoodRepository _typeOfFoodRepository;

        public FoodService
        (
            IFoodRepository foodRepository,
            IRestaurantRepository restaurantRepository,
            ITypeOfFoodRepository typeOfFoodRepository
        )
        {
            _foodRepository = foodRepository;
            _restaurantRepository = restaurantRepository;
            _typeOfFoodRepository = typeOfFoodRepository;
        }

        public void DeleteFood(string idFood, string idRestaurant)
        {
            if (GetRestaurantOfFood(idFood).Id == idRestaurant)
            {
                _foodRepository.Delete(idFood);
            }
            else
            {
                throw new System.Exception("Restaurant can't access this food.");
            }
            
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
            if(food == null)
            {
                throw new System.Exception("Bad food id.");
            }
            else
            {
                string idRestaurant = food.IdRestaurant;
                Restaurant restaurant = _restaurantRepository.GetById(idRestaurant);

                return RestaurantDto.FromEntity(restaurant);
            }
        }

        public TypeOfFood GetTypeOfFood(string id)
        {
            Food food = GetFoodById(id);
            string idTypeOfFood = food.IdTypeOfFood;
            TypeOfFood typeOfFood = _typeOfFoodRepository.GetById(idTypeOfFood);

            return typeOfFood;
        }

        public void RegisterFood(Food food)
        {
            _foodRepository.Add(food);
        }
    }
}
