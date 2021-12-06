using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.QueryParameters
{
    public class RestaurantParameters : QueryStringParameters
    {
        public string SortOrder { get; set; } = "name";
        private Coords _coords = new Coords { Longitude = 0, Latitude = 0 };
        public decimal Longitude { get => _coords.Longitude; set => _coords.Longitude = value; }
        public decimal Latitude { get => _coords.Latitude; set => _coords.Latitude = value; }
    }
}
