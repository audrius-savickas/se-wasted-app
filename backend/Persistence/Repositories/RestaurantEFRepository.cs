using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using Persistence.Utils;
using Services.Mappers;
using System;
using System.Linq;

namespace Persistence.Repositories
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
            return _context.Restaurants.Include(x => x.Foods).Select(x => x.ToDomain());
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
            return _context.Restaurants.Include(x => x.Foods).FirstOrDefault(x => x.Mail == mail.Value)?.ToDomain();
        }

        public IQueryable<Restaurant> GetRestaurantsNear(Coords coords)
        {
            return GetAllRestaurantsCloserThan(coords, Distances.NEAR);
        }

        public void Update(Restaurant restaurant)
        {
            if (GetByIdString(restaurant.Id) == null) return;

            var local = _context.Restaurants.Local.FirstOrDefault(x => x.Id == Guid.Parse(restaurant.Id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Restaurants.Update(restaurant.ToEntity());

            _context.SaveChanges();
        }

        private RestaurantEntity GetByIdString(string id)
        {
            return _context.Restaurants.Include(x => x.Foods).FirstOrDefault(x => x.Id == Guid.Parse(id));
        }
    }
}
