using Domain.Entities;
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
    public class RestaurantEFRepository : IRestaurantRepository
    {
        private readonly IDatabaseContext _context;
        public RestaurantEFRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public void Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant.ToEntity());
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
            return _context.Restaurants.Where(rest => rest.ToDomain().IsCloser(coords, distance))
                                       .Select(x => x.ToDomain());
        }

        public PagedList<Restaurant> GetAllWithPaging(RestaurantParameters restaurantParameters)
        {
            return PagedList<Restaurant>.ToPagedList(
                _context.Restaurants.Select(x => x.ToDomain()),
                restaurantParameters.PageNumber,
                restaurantParameters.PageSize);
        }

        public Restaurant GetById(string id)
        {
            return GetByIdString(id)?.ToDomain();
        }

        public Restaurant GetByMail(Mail mail)
        {
            return _context.Restaurants.FirstOrDefault(x => x.Mail == mail.Value)?.ToDomain();
        }

        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords)
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
