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
        private readonly IImageService _imageService;

        public FoodService
        (
            IFoodRepository foodRepository,
            IRestaurantRepository restaurantRepository,
            ITypeOfFoodRepository typeOfFoodRepository,
            IImageService imageService
        )
        {
            _foodRepository = foodRepository;
            _restaurantRepository = restaurantRepository;
            _typeOfFoodRepository = typeOfFoodRepository;
            _imageService = imageService;
        }

        public void DeleteFood(string idFood, string idRestaurant)
        {
            if (GetRestaurantOfFood(idFood).Id == idRestaurant)
            {
                _foodRepository.Delete(idFood);
            }
            else
            {
                throw new Exception("Restaurant can't access given food.");
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
                throw new Exception("Invalid food id.");
            }

            updatedFood.TypesOfFood = GetValidTypesOfFood(updatedFood.TypesOfFood);

            _foodRepository.Update(updatedFood);
        }

        public RestaurantDto GetRestaurantOfFood(string idFood)
        {
            Food food = GetFoodById(idFood);
            if(food == null)
            {
                throw new Exception("Invalid id.");
            }

            string idRestaurant = food.IdRestaurant;
            Restaurant restaurant = _restaurantRepository.GetById(idRestaurant);
            Image image = _imageService.ReadImage(restaurant.ImageSrc);

            return RestaurantDto.FromEntity(restaurant, image);
        }

        public IEnumerable<TypeOfFood> GetTypesOfFood(string idFood)
        {
            Food food = GetFoodById(idFood);
            if (food == null)
            {
                throw new Exception("Invalid id.");
            }
            else
            {
                return food.TypesOfFood;
            }
            
        }

        public string RegisterFood(Food food)
        {
            // Check if restaurant is valid
            if (_restaurantRepository.GetById(food.IdRestaurant) == null)
            {
                throw new Exception("Invalid restaurant id.");
            }
            // Generate id for food item
            string id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);

            var types = GetValidTypesOfFood(food.TypesOfFood);

            Food newFood = new Food
            {
                Name = food.Name,
                Price = food.Price,
                Id = id,
                IdRestaurant = food.IdRestaurant,
                TypesOfFood = types
            };

            _foodRepository.Add(newFood);
            return id;
        }

        private IEnumerable<TypeOfFood> GetValidTypesOfFood(IEnumerable<TypeOfFood> types)
        {
            if (types.Any(type => _typeOfFoodRepository.GetById(type.Id) == null))
            {
                throw new Exception("Contains invalid food type id.");
            }

            var typeIds = types.Select(x => x.Id);
            var validTypes = _typeOfFoodRepository.GetAll().Where(x => typeIds.Contains(x.Id));

            return validTypes;
        }
    }
}
