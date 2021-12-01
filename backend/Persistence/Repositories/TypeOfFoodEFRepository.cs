using Domain.Entities;
using Domain.Models;
using Persistence;
using Persistence.Interfaces;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public class TypeOfFoodEFRepository : ITypeOfFoodRepository
    {
        private readonly IDatabaseContext _context;
        public TypeOfFoodEFRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public void Add(TypeOfFood entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TypeOfFood> GetAll()
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
