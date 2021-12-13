using Domain.Models;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ReservationEFRepository : IReservationRepository
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Reservation> GetAll()
        {
            throw new NotImplementedException();
        }

        public Reservation GetById(string id)
        {
            throw new NotImplementedException();
        }

        public string Insert(Reservation model)
        {
            throw new NotImplementedException();
        }

        public void Update(Reservation model)
        {
            throw new NotImplementedException();
        }
    }
}
