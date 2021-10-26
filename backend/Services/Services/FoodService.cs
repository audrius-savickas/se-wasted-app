using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Persistence.Interfaces;
using Contracts.DTOs;
using Services.Interfaces;
using System;

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
                throw new System.Exception("Restaurant can't access given food.");
            }
            
        }

        public IEnumerable<Food> GetAllFood()
        {
            return _foodRepository.GetAll().ToList();
        }

        public Food GetFoodById(string idFood)
        {
            return _foodRepository.GetById(idFood);
        }

        public void UpdateFood(Food updatedFood)
        {
            if (GetFoodById(updatedFood.Id) == null)
            {
                throw new Exception("Invalid id.");
            }

            _foodRepository.Update(updatedFood);
        }

        public RestaurantDto GetRestaurantOfFood(string idFood)
        {
            Food food = GetFoodById(idFood);
            if(food == null)
            {
                throw new Exception("Invalid id.");
            }
            else
            {
                string idRestaurant = food.IdRestaurant;
                Restaurant restaurant = _restaurantRepository.GetById(idRestaurant);

                return RestaurantDto.FromEntity(restaurant);
            }
        }

        public TypeOfFood GetTypeOfFood(string idFood)
        {
            Food food = GetFoodById(idFood);
            if (food == null)
            {
                throw new Exception("Invalid id.");
            }
            else
            {
                string idTypeOfFood = food.TypesOfFood;
                TypeOfFood typeOfFood = _typeOfFoodRepository.GetById(idTypeOfFood);

                return typeOfFood;
            }
            
        }

        public string RegisterFood(Food food)
        {
            // Check if restaurant is valid
            if (_restaurantRepository.GetById(food.IdRestaurant) == null)
            {
                throw new System.Exception("Invalid restaurant id.");
            }
            // Check if typeOfFood is valid
            if (_typeOfFoodRepository.GetById(food.TypesOfFood) == null)
            {
                throw new System.Exception("Invalid food type id.");
            }

            // Generate id for food item
            string id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);

            Food newFood = new Food
            {
                Name = food.Name,
                Price = food.Price,
                Id = id,
                IdRestaurant = food.IdRestaurant,
                TypesOfFood = food.TypesOfFood,
            };

            _foodRepository.Add(newFood);
            return id;
        }
    }
}
