using Domain.Entities;
using Domain.Models;
using Services.Repositories;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Services.Tests.ServicesUnitTests
{
    public class TypeOfFoodServiceUnitTests : UnitTests
    {
        private readonly TypeOfFoodEFRepository _typeOfFoodEFRepository;
        private TypeOfFood _typeOfFood;
        private TypeOfFoodEntity _typeOfFoodEntity;

        public TypeOfFoodServiceUnitTests()
        {
            _typeOfFoodEFRepository = new TypeOfFoodEFRepository(_context);
            _typeOfFood = GetSampleTypeOfFood();
            _typeOfFoodEntity = GetSampleTypeOfFoodEntity();
        }

        [Fact]
        public void AddTypeOfFood_ValidTypeOfFood_ReturnsTypeOfFoodId()
        {
            var sut = new TypeOfFoodService(_typeOfFoodEFRepository);

            var typeId = sut.AddTypeOfFood(_typeOfFood);

            var addedTypeOfFood = _context.TypesOfFood.Find(Guid.Parse(typeId));
            Assert.NotNull(addedTypeOfFood);
            Assert.Equal(_typeOfFood.Name, addedTypeOfFood.Name);
        }

        [Fact]
        public void DeleteTypeOfFood_ValidId_DeletesTypeOfFoodFromDataBase()
        {
            _context.TypesOfFood.Add(_typeOfFoodEntity);
            _context.SaveChanges();
            var sut = new TypeOfFoodService(_typeOfFoodEFRepository);

            sut.DeleteTypeOfFood(_typeOfFoodEntity.Id.ToString());

            var addedTypeOfFood = _context.TypesOfFood.Find(_typeOfFoodEntity.Id);
            Assert.Null(addedTypeOfFood);
        }

        [Fact]
        public void DeleteTypeOfFood_InvalidId_DoesntChangeTheDataBase()
        {
            _context.TypesOfFood.Add(_typeOfFoodEntity);
            _context.SaveChanges();
            var sut = new TypeOfFoodService(_typeOfFoodEFRepository);
            try
            {
                sut.DeleteTypeOfFood(Guid.NewGuid().ToString());
            }
            catch (Exception) { }

            var addedTypeOfFood = _context.TypesOfFood.Find(_typeOfFoodEntity.Id);
            Assert.NotNull(addedTypeOfFood);
            Assert.Equal(_typeOfFoodEntity.Name, addedTypeOfFood.Name);
        }

        [Fact]
        public void GetAllTypesOfFood_OneTypeOfFoodInDataBase_ReturnsAllTypesOfFoodInDataBase()
        {
            _context.TypesOfFood.Add(_typeOfFoodEntity);
            _context.SaveChanges();
            var sut = new TypeOfFoodService(_typeOfFoodEFRepository);

            var types = sut.GetAllTypesOfFood();

            Assert.Single(types);
            Assert.Equal(_typeOfFoodEntity.Id.ToString(), types.First().Id);
        }

        [Fact]
        public void GetAllTypesOfFood_MoreThanOneTypeOfFoodInDataBase_ReturnsAllTypesOfFoodInDataBase()
        {
            _context.TypesOfFood.Add(_typeOfFoodEntity);
            var otherTypeOfFood = GetSampleTypeOfFoodEntity();
            var otherTypeOfFoodId = Guid.NewGuid();
            otherTypeOfFood.Id = otherTypeOfFoodId;
            _context.TypesOfFood.Add(otherTypeOfFood);
            _context.SaveChanges();
            var sut = new TypeOfFoodService(_typeOfFoodEFRepository);

            var types = sut.GetAllTypesOfFood();

            Assert.NotEmpty(types);
            Assert.Equal(_typeOfFoodEntity.Id.ToString(), types.First().Id);
            Assert.Equal(otherTypeOfFoodId.ToString(), types.ToList()[1].Id);
        }

        [Fact]
        public void GetTypeOfFoodById_ValidId_ReturnsTypeOfFood()
        {
            _context.TypesOfFood.Add(_typeOfFoodEntity);
            _context.SaveChanges();
            var sut = new TypeOfFoodService(_typeOfFoodEFRepository);

            var type = sut.GetTypeOfFoodById(_typeOfFoodEntity.Id.ToString());

            Assert.NotNull(type);
            Assert.Equal(_typeOfFoodEntity.Id.ToString(), type.Id);
        }

        [Fact]
        public void GetTypeOfFoodById_InvalidId_ReturnsNull()
        {
            _context.TypesOfFood.Add(_typeOfFoodEntity);
            _context.SaveChanges();
            var sut = new TypeOfFoodService(_typeOfFoodEFRepository);

            var type = sut.GetTypeOfFoodById(Guid.NewGuid().ToString());

            Assert.Null(type);
        }
    }
}
