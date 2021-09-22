using System;
namespace console_wasted_app.Controller.Entities
{
    public struct Coords
    {
        public decimal Latitude;
        public decimal Altitude;

        public Coords( decimal latitude, decimal altitude )
        {
            Latitude = latitude;
            Altitude = altitude;
        }
    }
}
