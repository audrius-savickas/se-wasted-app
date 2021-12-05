using Domain.Entities;
using Domain.Models;
using Persistence;
using Persistence.Interfaces;
using Persistence.Utils;
using Services.Mappers;
using System;
using System.Linq;

namespace Services.Repositories
{
    public class TypeOfFoodEFRepository : ITypeOfFoodRepository
    {
        private readonly IDatabaseContext _context;
        public TypeOfFoodEFRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public string Insert(TypeOfFood typeOfFood)
        {
            typeOfFood.Id = IdGenerator.GenerateUniqueId();
            _context.TypesOfFood.Add(typeOfFood.ToEntity());
            _context.SaveChanges();
            return typeOfFood.Id;
        }

        public void Delete(string id)
        {
            TypeOfFoodEntity entity = GetByIdString(id);
            _context.TypesOfFood.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<TypeOfFood> GetAll()
        {
            return _context.TypesOfFood.Select(x => x.ToDomain());
        }

        public TypeOfFood GetById(string id)
        {
            return GetByIdString(id)?.ToDomain();
        }

        public void Update(TypeOfFood entity)
        {
            throw new NotImplementedException();
        }

        private TypeOfFoodEntity GetByIdString(string id)
        {
            return _context.TypesOfFood.Find(Guid.Parse(id));
        }
    }
}
