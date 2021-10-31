using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class RestaurantRegisterRequest
    {
        public string Name { get; set; }

        public Coords Coords { get; set; }

        public string Address { get; set; }


        public RestaurantRegisterRequest(string name, string address, Coords coords)
        {
            Name = name;
            Address = address;
            Coords = coords;
        }
    }
}
