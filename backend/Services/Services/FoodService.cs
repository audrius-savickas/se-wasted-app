using Contracts.DTOs;
using Domain.Entities;
using Persistence.Interfaces;
using Services.Exceptions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
                throw new AuthorizationException("Restaurant can't access given food.");
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
                throw new EntityNotFoundException("Invalid food id.");
            }
            ValidateDecreaseType(updatedFood.DecreaseType);

            updatedFood.TypesOfFood = GetValidTypesOfFood(updatedFood.TypesOfFood);

            _foodRepository.Update(updatedFood);
        }

        public RestaurantDto GetRestaurantOfFood(string idFood)
        {
            Food food = GetFoodById(idFood);
            if (food == null)
            {
                throw new EntityNotFoundException("Invalid food id.");
            }
            else
            {
                string idRestaurant = food.IdRestaurant;
                Restaurant restaurant = _restaurantRepository.GetById(idRestaurant);

                return RestaurantDto.FromEntity(restaurant);
            }
        }

        public IEnumerable<TypeOfFood> GetTypesOfFood(string idFood)
        {
            Food food = GetFoodById(idFood);
            if (food == null)
            {
                throw new EntityNotFoundException("Invalid food id.");
            }
            else
            {
                return food.TypesOfFood;
            }

        }

        public string RegisterFood(Food food, Func<string> generateId)
        {
            ValidateDecreaseType(food.DecreaseType);

            // Check if restaurant is valid
            if (_restaurantRepository.GetById(food.IdRestaurant) == null)
            {
                throw new EntityNotFoundException("Invalid restaurant id.");
            }

            food.CheckIfImageUrlIsSet();

            // Generate id for food item
            food.Id = generateId();

            food.TypesOfFood = GetValidTypesOfFood(food.TypesOfFood);

            _foodRepository.Add(food);
            return food.Id;
        }

        private IEnumerable<TypeOfFood> GetValidTypesOfFood(IEnumerable<TypeOfFood> types)
        {
            if (types.Any(type => _typeOfFoodRepository.GetById(type.Id) == null))
            {
                throw new ArgumentException("Contains invalid food type id.");
            }

            var typeIds = types.Select(x => x.Id);
            var validTypes = _typeOfFoodRepository.GetAll().Where(x => typeIds.Contains(x.Id));

            return validTypes;
        }

        private void ValidateDecreaseType(object value)
        {
            if (Enum.IsDefined(typeof(DecreaseType), value) == false)
            {
                throw new ArgumentException("Invalid price decrease type.");
            }
        }
    }
}
