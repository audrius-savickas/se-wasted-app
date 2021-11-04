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

        private const string DEFAULT_IMAGE_URL = "todo";

        public Restaurant() : base() { }

        public Restaurant(string id, string name, string address, Coords coords, Credentials credentials, string? imageURL = null)
            : base(id, name)
        {
            Address = address;
            Coords = coords ?? throw new ArgumentNullException(nameof(coords));
            Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            ImageURL = imageURL ?? DEFAULT_IMAGE_URL;
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
