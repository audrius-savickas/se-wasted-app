using System;

namespace Domain.Entities
{
    public class Coords
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public Coords(decimal longitude, decimal latitude)
        {
            if(MathF.Abs((float)latitude) > 90 || MathF.Abs((float)longitude) > 180)
            {
                throw new Exception("Invalid coordinates.");
            }

            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
