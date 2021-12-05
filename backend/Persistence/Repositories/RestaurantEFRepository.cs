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
    public class RestaurantEFRepository : IRestaurantRepository
    {
        private readonly DatabaseContext _context;
        public RestaurantEFRepository(DatabaseContext context)
        {
            _context = context;
        }
        public string Insert(Restaurant restaurant)
        {
            restaurant.Id = IdGenerator.GenerateUniqueId();
            _context.Restaurants.Add(restaurant.ToEntity());
            _context.SaveChanges();
            return restaurant.Id;
        }

        public void Delete(string id)
        {
            RestaurantEntity entity = GetByIdString(id);
            _context.Restaurants.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<Restaurant> GetAll()
        {
            return _context.Restaurants.Select(x => x.ToDomain());
        }

        public IQueryable<Restaurant> GetAllRestaurantsCloserThan(Coords coords, Distances distance)
        {
            return GetAll().ToList().Where(rest => rest.IsCloser(coords, distance)).AsQueryable();
        }

        public Restaurant GetById(string id)
        {
            return GetByIdString(id)?.ToDomain();
        }

        public Restaurant GetByMail(Mail mail)
        {
            return _context.Restaurants.FirstOrDefault(x => x.Mail == mail.Value)?.ToDomain();
        }

        public IQueryable<Restaurant> GetRestaurantsNear(Coords coords)
        {
            return GetAllRestaurantsCloserThan(coords, Distances.NEAR);
        }

        public void Update(Restaurant restaurant)
        {
            RestaurantEntity entity = GetByIdString(restaurant.Id);
            if (entity != null)
            {
                _context.Restaurants.Remove(entity);          // FIX: Ugly workaround for updating
                _context.Restaurants.Add(restaurant.ToEntity());
                _context.SaveChanges();
            }
        }

        private RestaurantEntity GetByIdString(string id)
        {
            return _context.Restaurants.Find(Guid.Parse(id));
        }
    }
}
