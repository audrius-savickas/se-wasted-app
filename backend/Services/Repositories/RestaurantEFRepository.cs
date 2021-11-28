using Domain.Entities;
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
    public class RestaurantEFRepository : IRestaurantRepository
    {
        private readonly IDatabaseContext _context;
        public RestaurantEFRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public void Add(Restaurant entity)
        {
            _context.Restaurants.Add(entity.ToEntity());
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            RestaurantEntity entity = GetByIdString(id);
            _context.Restaurants.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants.Select(x => x.ToDomain());
        }

        public IEnumerable<Restaurant> GetAllRestaurantsCloserThan(Coords coords, Distances distance)
        {
            throw new NotImplementedException();
        }

        public Restaurant GetById(string id)
        {
            return GetByIdString(id).ToDomain();
        }

        public Restaurant GetByMail(Mail mail)
        {
            return _context.Restaurants.FirstOrDefault(x => x.Mail == mail.Value).ToDomain();
        }

        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords)
        {
            throw new NotImplementedException();
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
