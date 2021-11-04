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

        public string ImageURL { get; set; }

        private const string DEFAULT_IMAGE_URL = "todo";


        public RestaurantRegisterRequest(string name, string address, Coords coords, string imageURL = DEFAULT_IMAGE_URL)
        {
            Name = name;
            Address = address;
            Coords = coords;
            ImageURL = imageURL;
        }
    }
}
