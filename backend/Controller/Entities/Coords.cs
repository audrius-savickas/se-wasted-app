using System;
namespace backend.Controller.Entities
{
    public struct Coords
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public Coords(decimal longitude, decimal latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        private decimal ManhattanDistance(Coords others)
        {
            return Math.Abs(Longitude - others.Longitude) + Math.Abs(Latitude - others.Latitude);
        }

        public bool IsNear(Coords others)
        {
            return (ManhattanDistance(others) <= (decimal)Distances.NEAR);
        }
    }
}
