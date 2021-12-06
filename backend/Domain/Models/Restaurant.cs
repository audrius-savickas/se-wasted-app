using Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Restaurant : BaseModel
    {
        public IEnumerable<Food> Foods { get; set; }
        public Coords Coords { get; set; }
        public string Address { get; set; }
        public Credentials Credentials { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        private const string DEFAULT_IMAGE_URL = "https://genesisairway.com/wp-content/uploads/2019/05/no-image.jpg";

        public Restaurant() : base() { }

        public Restaurant(string id, string name, string address, Coords coords, Credentials credentials, IEnumerable<Food> foods, string description = "", string imageURL = "")
            : base(id, name)
        {
            Foods = foods;
            Description = description;
            Address = address;
            Coords = coords ?? throw new ArgumentNullException(nameof(coords));
            Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            if(imageURL == "")
            {
                ImageURL = DEFAULT_IMAGE_URL;
            }
            else
            {
                ImageURL = imageURL;
            }
        }

        public bool IsNear(Coords coords)
        {
            _ = coords ?? throw new ArgumentNullException(nameof(coords));

            return IsCloser(coords, Distances.NEAR);
        }

        public bool IsCloser(Coords coords, Distances distance)
        {
            _ = coords ?? throw new ArgumentNullException(nameof(coords));

            return CoordsHelper.IsCloser(Coords, coords, distance);
        }

        public double DistanceTo(Coords coords)
        {
            coords ??= new Coords { Longitude = 0, Latitude = 0 };
            return CoordsHelper.HaversineDistanceKM(Coords, coords);
        }
    }
}
