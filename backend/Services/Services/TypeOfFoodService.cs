using Domain.Models;
using Persistence.File.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;

namespace Services.Services
{
    public class TypeOfFoodService : ITypeOfFoodService
    {
        private readonly ITypeOfFoodRepository _typeOfFoodRepository;

        public TypeOfFoodService(ITypeOfFoodRepository typeOfFoodRepository)
        {
            _typeOfFoodRepository = typeOfFoodRepository;
        }

        public void AddTypeOfFood(TypeOfFood newTypeOfFood)
        {
            _typeOfFoodRepository.Add(newTypeOfFood);
        }

        public void DeleteTypeOfFood(string id)
        {
            _typeOfFoodRepository.Delete(id);
        }

        public IEnumerable<TypeOfFood> GetAllTypesOfFood()
        {
            return _typeOfFoodRepository.GetAll();
        }

        public TypeOfFood GetTypeOfFoodById(string id)
        {
            return _typeOfFoodRepository.GetById(id);
        }

        public void UpdateTypeOfFood(TypeOfFood updatedTypeOfFood)
        {
            _typeOfFoodRepository.Update(updatedTypeOfFood);
        }
    }
}
