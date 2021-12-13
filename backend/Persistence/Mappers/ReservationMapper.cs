using Contracts.DTOs;
using Domain.Entities;
using Domain.Models;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Mappers
{
    public static class ReservationMapper
    {
        public static Reservation ToDomain(this ReservationEntity from)
        {
            return new Reservation(
                from.Id.ToString(),
                from.IsCancelled,
                from.Food.ToDomain(),
                from.Food.Restaurant.ToDomain(),
                from.Customer.ToDomain());
        }

        public static ReservationEntity ToEntity(this Reservation from)
        {
            return new ReservationEntity
            {
                FoodId = Guid.Parse(from.Food.Id),
                CustomerId = Guid.Parse(from.Customer.Id),
                Id = Guid.Parse(from.Id),
                IsCancelled = from.IsCancelled,
                Price = from.Price,
                ReservedAt = from.ReservedAt,
            };
        }
        public static ReservationDto ToDTO(this Reservation from)
        {
            _ = from ?? throw new ArgumentNullException(nameof(from));

            return new ReservationDto(
                from.Food.Id,
                from.Customer.Id,
                from.ReservedAt,
                from.Price);
        }
    }
}
