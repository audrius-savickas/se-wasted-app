using Contracts.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Mappers
{
    public static class ReservationMapper
    {
        public static ReservationDto ToDTO(this Reservation from)
        {
            _ = from ?? throw new ArgumentNullException(nameof(from));

            return new ReservationDto(
                from.Id,
                from.Food.Id,
                from.Restaurant.Id,
                from.Customer.Id,
                from.ReservedAt,
                from.Price);
        }
    }
}
