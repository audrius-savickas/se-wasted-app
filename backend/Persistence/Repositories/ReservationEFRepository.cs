using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using Persistence.Mappers;
using Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ReservationEFRepository : IReservationRepository
    {
        private readonly DatabaseContext _context;
        public ReservationEFRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Reservation> GetAll()
        {
            return _context.Reservations.Include(x => x.Customer)
                .Include(x => x.Food)
                .ThenInclude(x => x.Restaurant)
                .Select(x => x.ToDomain());
        }

        public Reservation GetByFoodAndCustomer(string foodId, string customerId)
        {
            return _context.Reservations.Include(x => x.Customer)
                .Include(x => x.Food)
                .ThenInclude(x => x.Restaurant)
                .FirstOrDefault(x => x.FoodId == Guid.Parse(foodId) && x.CustomerId == Guid.Parse(customerId))?.ToDomain();
        }

        public Reservation GetById(string id)
        {
            return GetByIdString(id)?.ToDomain();
        }

        public string Insert(Reservation reservation)
        {
            reservation.Id = IdGenerator.GenerateUniqueId();

            _context.Reservations.Add(reservation.ToEntity());
            _context.SaveChanges();
            return reservation.Id;
        }

        public void Update(Reservation reservation)
        {
            if (GetByIdString(reservation.Id) == null) return;

            var local = _context.Reservations.Local.FirstOrDefault(x => x.Id == Guid.Parse(reservation.Id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Reservations.Update(reservation.ToEntity());

            _context.SaveChanges();
        }

        private ReservationEntity GetByIdString(string id)
        {
            return _context.Reservations.Include(x => x.Customer)
                .Include(x => x.Food)
                .ThenInclude(x => x.Restaurant)
                .FirstOrDefault(x => x.Id == Guid.Parse(id));
        }
    }
}
