using Domain.Models;
using Persistence.EF;
using Persistence.File.Interfaces;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public class FoodEFRepository : IFoodRepository
    {
        private readonly IDatabaseContext _context;
        public FoodEFRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public void Add(Food entity)
        {
            _context.Foods.Add(entity.ToEntity());
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Food> GetAll()
        {
            throw new NotImplementedException();
        }

        public Food GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Food entity)
        {
            throw new NotImplementedException();
        }
    }
}
