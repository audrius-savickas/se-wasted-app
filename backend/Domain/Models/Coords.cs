using System;

namespace Domain.Models
{
    public class Coords
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public Coords() { }

        public Coords(decimal longitude, decimal latitude)
        {
            if (MathF.Abs((float)latitude) > 90 || MathF.Abs((float)longitude) > 180)
            {
                throw new ArgumentException("Invalid coordinates.");
            }

            Longitude = longitude;
            Latitude = latitude;
        }

    }
}
