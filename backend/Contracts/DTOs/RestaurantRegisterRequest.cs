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

        private const string DEFAULT_IMAGE_URL = "https://www.google.lt/url?sa=i&url=https%3A%2F%2Fcommons.wikimedia.org%2Fwiki%2FFile%3ANo-Image-Placeholder.svg&psig=AOvVaw1uR0Zny7PYJn8sTl750JMW&ust=1636147269746000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCKCc6rvS__MCFQAAAAAdAAAAABAD";


        public RestaurantRegisterRequest(string name, string address, Coords coords, string imageURL = DEFAULT_IMAGE_URL)
        {
            Name = name;
            Address = address;
            Coords = coords;
            ImageURL = imageURL;
        }
    }
}
