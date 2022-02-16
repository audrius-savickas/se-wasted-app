using Domain.Entities;
using Domain.Models;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests;
using Xunit;

namespace Persistence.Tests.RepositoriesUnitTests
{
    public class TypeOfFoodEFRepositoryUnitTests : UnitTests
    {
        private TypeOfFood TypeOfFood { get; set; }
        private TypeOfFoodEntity TypeOfFoodEntity { get; set; }

        public TypeOfFoodEFRepositoryUnitTests()
        {
            TypeOfFood = GetSampleTypeOfFood();
            TypeOfFoodEntity = GetSampleTypeOfFoodEntity();
        }

        [Fact]
        public void Insert_InsertTypeOfFoodToDataBase()
        {
            var sut = new TypeOfFoodEFRepository(_context);

            sut.Insert(TypeOfFood);

            List<TypeOfFoodEntity> typesOfFood = _context.TypesOfFood.ToList();
            Assert.Single(typesOfFood);
            Assert.Equal(TypeOfFood.Id, typesOfFood.First().Id.ToString());
        }

        [Fact]
        public void Delete_DeletesExistingTypeOfFood()
        {
            _context.TypesOfFood.Add(TypeOfFoodEntity);
            _context.SaveChanges();
            var sut = new TypeOfFoodEFRepository(_context);

            sut.Delete(TypeOfFoodEntity.Id.ToString());

            Assert.Empty(_context.TypesOfFood.ToList());
        }

        [Fact]
        public void GetAll_OneTypeOfFoodInDataBase_GetsAllExistingTypesOfFood()
        {
            _context.TypesOfFood.Add(TypeOfFoodEntity);
            _context.SaveChanges();
            var sut = new TypeOfFoodEFRepository(_context);

            Assert.Single(sut.GetAll());
        }

        [Fact]
        public void GetAll_MoreThanOneTypeOfFoodInDataBase_GetsAllExistingTypesOfFood()
        {
            _context.TypesOfFood.Add(TypeOfFoodEntity);
            var otherTypeOfFoodId = Guid.NewGuid();
            var otherTypeOfFood = GetSampleTypeOfFoodEntity();
            otherTypeOfFood.Id = otherTypeOfFoodId;
            _context.TypesOfFood.Add(otherTypeOfFood);
            _context.SaveChanges();
            var sut = new TypeOfFoodEFRepository(_context);

            IQueryable<TypeOfFood> typesOfFood = sut.GetAll();

            Assert.NotEmpty(typesOfFood);
            Assert.Equal(TypeOfFoodEntity.Id.ToString(), typesOfFood.ToList()[0].Id);
            Assert.Equal(otherTypeOfFoodId.ToString(), typesOfFood.ToList()[1].Id);
        }

        [Fact]
        public void GetById_TypeOfFoodExistsInDataBase_ReturnsTypeOfFood()
        {
            _context.TypesOfFood.Add(TypeOfFoodEntity);
            _context.SaveChanges();
            var sut = new TypeOfFoodEFRepository(_context);

            var typeOfFood = sut.GetById(TypeOfFoodEntity.Id.ToString());

            Assert.Equal(TypeOfFoodEntity.Id.ToString(), typeOfFood.Id);
        }

        [Fact]
        public void GetById_TypeOfFoodDoesntExistsInDataBase_ReturnsNull()
        {
            var sut = new TypeOfFoodEFRepository(_context);

            var typeOfFood = sut.GetById(Guid.NewGuid().ToString());

            Assert.Null(typeOfFood);
        }

        [Fact]
        public void Update()
        {
            var sut = new TypeOfFoodEFRepository(_context);

            Assert.Throws<NotImplementedException>(() => sut.Update(TypeOfFood));

        }
    }
}
