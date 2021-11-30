﻿using Domain.Entities;
using Domain.Models;
using Domain.Models.QueryParameters;
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
    public class FoodEFRepository : IFoodRepository
    {
        private readonly IDatabaseContext _context;
        public FoodEFRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public void Add(Food food)
        {
            _context.Foods.Add(food.ToEntity());
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            FoodEntity entity = GetByIdString(id);
            _context.Foods.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Food> GetAll()
        {
            return _context.Foods.Select(x => x.ToDomain());
        }

        public PagedList<Food> GetAllWithPaging(FoodParameters foodParameters)
        {
            return PagedList<Food>.ToPagedList(
                _context.Foods.Select(x => x.ToDomain()), 
                foodParameters.PageNumber, 
                foodParameters.PageSize);
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

        private FoodEntity GetByIdString(string id)
        {
            return _context.Foods.Find(Guid.Parse(id));
        }
    }
}