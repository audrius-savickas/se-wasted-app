using Domain.Helpers;

namespace Domain.Entities
{
    public class Restaurant : BaseEntity
    {
        public Coords Coords { get; set; }
        public string Address { get; set; }
        public Credentials Credentials { get; set; }
        public string ImageSrc { get; set; }

        public Restaurant() : base() { }

        public Restaurant
        (
            string id,
            string name,
            string address,
            Coords coords,
            Credentials creds,
            string imageSrc
        )
            : base(id, name)
        {
            Address = address;
            Coords = coords;
            Credentials = creds;
            ImageSrc = imageSrc;
        }

        public bool IsNear(Coords coords)
        {
            return IsCloser(coords, Distances.NEAR);
        }

        public bool IsCloser(Coords coords, Distances distance)
        {
            return CoordsHelper.IsCloser(Coords, coords, distance);
        }
    }
}
