using Domain.Helpers;
using System;

namespace Domain.Entities
{
    public class Restaurant : BaseEntity
    {
        public Coords Coords { get; set; }

        public string Address { get; set; }

        public Credentials Credentials { get; set; }

        public Restaurant() : base() { }

        public Restaurant(string id, string name, string address, Coords coords, Credentials creds)
            : base(id, name)
        {
            Address = address;
            if(coords == null)
            {
                throw new Exception("Invalid coordinates");
            }
            else
            {
                Coords = coords;
            }
            
            if(creds == null)
            {
                throw new Exception("Invalid credentials");
            }
            else
            {
                Credentials = creds;
            }
        }

        public bool IsNear(Coords coords)
        {
            if (coords == null)
            {
                throw new Exception("Invalid coordinates");
            }
            else
            {
                return IsCloser(coords, Distances.NEAR);
            }
        }

        public bool IsCloser(Coords coords, Distances distance)
        {
            if(coords == null)
            {
                throw new Exception("Invalid coordinates");
            }
            else
            {
                return CoordsHelper.IsCloser(Coords, coords, distance);
            }
        }
    }
}
