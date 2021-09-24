using System;
namespace console_wasted_app.Controller.Entities
{
    public struct Coords
    {
        public decimal Latitude { get; set; }
        public decimal Altitude { get; set; }

        public Coords( decimal latitude, decimal altitude )
        {
            Latitude = latitude;
            Altitude = altitude;
        }

        private decimal ManhattanDistance(Coords others)
        {
            return Math.Abs(Latitude - others.Latitude) + Math.Abs(Altitude - others.Altitude);
        }

        public bool IsNear(Coords others)
        {
            return (ManhattanDistance(others) <= (decimal)Distances.NEAR);
        }
    }
}
