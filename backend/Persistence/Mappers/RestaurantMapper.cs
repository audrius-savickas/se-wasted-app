﻿using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappers
{
    public static class RestaurantMapper
    {
        public static Restaurant ToDomain(this RestaurantEntity from)
        {
            string restaurantId = from.Id.ToString();
            Coords coords = new Coords { Longitude = from.Longitude, Latitude = from.Latitude };
            Credentials credentials = new Credentials 
            {
                Mail = new Mail { Value = from.Mail },
                Password = new Password { Value = from.Password }
            };

            return new Restaurant(
                restaurantId,
                from.Name,
                from.Address,
                coords,
                credentials,
                from.Description,
                from.ImageURL);
        }

        public static RestaurantEntity ToEntity(this Restaurant from)
        {
            // FIX: foods not set
            return new RestaurantEntity
            {
                Id = Guid.Parse(from.Id),
                Name = from.Name,
                Longitude = from.Coords.Longitude,
                Latitude = from.Coords.Latitude,
                Address = from.Address,
                Mail = from.Credentials.Mail.Value,
                Password = from.Credentials.Password.Value,
                Description = from.Description,
                ImageURL = from.ImageURL,
            };
        }
    }
}