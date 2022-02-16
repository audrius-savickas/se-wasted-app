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
            _ = from ?? throw new ArgumentNullException(nameof(from));

            return new Reservation(
                from.Id.ToString(),
                from.IsCancelled,
                from.FoodId.ToString(),
                from.Food.RestaurantId.ToString(),
                from.CustomerId.ToString(),
                from.Price,
                from.ReservedAt);
        }

        public static ReservationEntity ToEntity(this Reservation from)
        {
            _ = from ?? throw new ArgumentNullException(nameof(from));

            return new ReservationEntity
            {
                FoodId = Guid.Parse(from.FoodId),
                CustomerId = Guid.Parse(from.CustomerId),
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
                from.FoodId,
                from.CustomerId,
                from.ReservedAt,
                from.Price);
        }
    }
}
