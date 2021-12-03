using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Services.Tests.Repositories.UnitTests
{
    public class TypeOfFoodEFRepositoryUnitTests
    {
        private readonly DatabaseContext _context;

        public TypeOfFoodEFRepositoryUnitTests()
        {
            DbContextOptionsBuilder<DatabaseContext> dbOptions = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            _context = new DatabaseContext(dbOptions.Options);
        }

        private Guid typeOfFoodId = Guid.NewGuid();
        private const string typeOfFoodName = "TypeOfFoodTestName";

        private TypeOfFood GetSampleTypeOfFood(Guid id)
        {
            var typeOfFood = new TypeOfFood
            {
                Id = id.ToString(),
                Name = typeOfFoodName
            };
            return typeOfFood;
        }

        private TypeOfFoodEntity GetSampleTypeOfFoodEntity(Guid id)
        {
            var typeOfFoodEntity = new TypeOfFoodEntity
            {
                Id = id,
                Name = typeOfFoodName
            };
            return typeOfFoodEntity;
        }

        [Fact]
        public void Insert_InsertTypeOfFoodToDataBase()
        {
            var sut = new TypeOfFoodEFRepository(_context);
            var typeOfFood = GetSampleTypeOfFood(typeOfFoodId);

            sut.Insert(typeOfFood);

            List<TypeOfFoodEntity> typesOfFood = _context.TypesOfFood.ToList();
            Assert.Single(typesOfFood);
            Assert.Equal(typeOfFood.Id, typeOfFoodId.ToString());
        }

        [Fact]
        public void Delete_DeletesExistingTypeOfFood()
        {
            _context.TypesOfFood.Add(GetSampleTypeOfFoodEntity(typeOfFoodId));
            _context.SaveChanges();
            var sut = new TypeOfFoodEFRepository(_context);

            sut.Delete(typeOfFoodId.ToString());

            List<TypeOfFoodEntity> typesOfFood = _context.TypesOfFood.ToList();
            Assert.Empty(typesOfFood);
        }

        [Fact]
        public void GetAll_OneTypeOfFoodInDataBase_GetsAllExistingTypesOfFood()
        {
            _context.TypesOfFood.Add(GetSampleTypeOfFoodEntity(typeOfFoodId));
            _context.SaveChanges();
            var sut = new TypeOfFoodEFRepository(_context);

            IEnumerable<TypeOfFood> typesOfFood = sut.GetAll();

            Assert.Single(typesOfFood);
        }

        [Fact]
        public void GetAll_MoreThanOneTypeOfFoodInDataBase_GetsAllExistingTypesOfFood()
        {
            _context.TypesOfFood.Add(GetSampleTypeOfFoodEntity(typeOfFoodId));
            var otherTypeOfFoodId = Guid.NewGuid();
            _context.TypesOfFood.Add(GetSampleTypeOfFoodEntity(otherTypeOfFoodId));
            _context.SaveChanges();
            var sut = new TypeOfFoodEFRepository(_context);

            IEnumerable<TypeOfFood> typesOfFood = sut.GetAll();

            Assert.NotEmpty(typesOfFood);
            Assert.Equal(typeOfFoodId.ToString(), typesOfFood.ToList()[0].Id);
            Assert.Equal(otherTypeOfFoodId.ToString(), typesOfFood.ToList()[1].Id);
        }

        [Fact]
        public void GetById_TypeOfFoodExistsInDataBase_ReturnsTypeOfFood()
        {
            _context.TypesOfFood.Add(GetSampleTypeOfFoodEntity(typeOfFoodId));
            _context.SaveChanges();
            var sut = new TypeOfFoodEFRepository(_context);

            var typeOfFood = sut.GetById(typeOfFoodId.ToString());

            Assert.Equal(typeOfFoodId.ToString(), typeOfFood.Id);
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

            Assert.Throws<NotImplementedException>(() => sut.Update(GetSampleTypeOfFood(typeOfFoodId)));

        }
    }
}
