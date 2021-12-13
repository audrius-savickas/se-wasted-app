using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using Persistence.Mappers;
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

        public Reservation GetById(string id)
        {
            return GetByIdString(id)?.ToDomain();
        }

        public string Insert(Reservation model)
        {
            throw new NotImplementedException();
        }

        public void Update(Reservation model)
        {
            throw new NotImplementedException();
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
