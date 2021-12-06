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
        public Coords Coords { get; set; } = new Coords { Longitude = 0, Latitude = 0 };
    }
}
