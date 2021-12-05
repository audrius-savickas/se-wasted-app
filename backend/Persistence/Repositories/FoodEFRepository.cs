using Domain.Entities;
using Domain.Models;
using Persistence;
using Persistence.Interfaces;
using Persistence.Utils;
using Services.Mappers;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Repositories
{
    public class FoodEFRepository : IFoodRepository
    {
        private readonly IDatabaseContext _context;
        public FoodEFRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public string Insert(Food food)
        {
            food.Id = IdGenerator.GenerateUniqueId();
            _context.Foods.Add(food.ToEntity());
            _context.SaveChanges();
            return food.Id;
        }

        public void Delete(string id)
        {
            FoodEntity entity = GetByIdString(id);
            _context.Foods.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<Food> GetAll()
        {
            return _context.Foods.Select(x => x.ToDomain());
        }

        public Food GetById(string id)
        {
            return GetByIdString(id)?.ToDomain();
        }

        public void Update(Food food)
        {
            FoodEntity entity = GetByIdString(food.Id);
            if (entity != null)
            {
                _context.Foods.Remove(entity);          // FIX: Ugly workaround for updating
                _context.Foods.Add(food.ToEntity());
                _context.SaveChanges();
            }
        }

        public IQueryable<Food> GetFoodFromRestaurant(string idRestaurant)
        {
            return _context.Foods.Where(food => food.RestaurantId == Guid.Parse(idRestaurant)).Select(x => x.ToDomain());
        }

        private FoodEntity GetByIdString(string id)
        {
            return _context.Foods.Find(Guid.Parse(id));
        }
    }
}
