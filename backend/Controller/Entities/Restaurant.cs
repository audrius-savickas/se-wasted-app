using wasted_app.Controller.Entities;

namespace backend.Controller.Entities
{
    public class Restaurant : BaseEntity
    {
        public Coords Coords { get; set; }
        public Credentials Credentials { get; set; }

        public Restaurant() : base() { }

        public Restaurant(string id, string name, Coords coords, Credentials creds)
            : base(id, name)
        {
            Coords = coords;
            Credentials = creds;
        }

        public bool IsNear(Coords coords)
        {
            return Coords.IsNear(coords);
        }
    }
}
