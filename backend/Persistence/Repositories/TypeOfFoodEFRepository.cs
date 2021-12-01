using Domain.Entities;
using Domain.Models;
using Persistence;
using Persistence.Interfaces;
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
        public string Insert(TypeOfFood entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
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
