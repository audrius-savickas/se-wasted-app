using Domain.Helpers;
using System;

namespace Domain.Entities
{
    public class Restaurant : BaseEntity
    {
        public Coords Coords { get; set; }

        public string Address { get; set; }

        public Credentials Credentials { get; set; }
        
        public string ImageURL { get; set; }

        private const string DEFAULT_IMAGE_URL = "https://www.google.lt/url?sa=i&url=https%3A%2F%2Fcommons.wikimedia.org%2Fwiki%2FFile%3ANo-Image-Placeholder.svg&psig=AOvVaw1uR0Zny7PYJn8sTl750JMW&ust=1636147269746000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCKCc6rvS__MCFQAAAAAdAAAAABAD";

        public Restaurant() : base() { }

        public Restaurant(string id, string name, string address, Coords coords, Credentials credentials, string imageURL = DEFAULT_IMAGE_URL)
            : base(id, name)
        {
            Address = address;
            Coords = coords ?? throw new ArgumentNullException(nameof(coords));
            Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            ImageURL = imageURL;
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
    }
}
