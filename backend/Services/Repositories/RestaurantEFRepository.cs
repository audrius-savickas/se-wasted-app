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
            throw new NotImplementedException();
        }

        public IEnumerable<Restaurant> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Restaurant> GetAllRestaurantsCloserThan(Coords coords, Distances distance)
        {
            throw new NotImplementedException();
        }

        public Restaurant GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Restaurant GetByMail(Mail mail)
        {
            return null;
        }

        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords)
        {
            throw new NotImplementedException();
        }

        public void Update(Restaurant entity)
        {
            throw new NotImplementedException();
        }
    }
}
