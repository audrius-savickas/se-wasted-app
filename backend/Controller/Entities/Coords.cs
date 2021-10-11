using System;
namespace backend.Controller.Entities
{
    public struct Coords
    {
        private const double AVERAGE_RADIUS_OF_EARTH_KM = 6371;

        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public Coords(decimal longitude, decimal latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        private double HaversineDistanceKM(Coords others)
        {
            double latDistance = ToRadians(Latitude - others.Latitude);
            double lngDistance = ToRadians(Longitude - others.Longitude);

            double a =
                Math.Sin((double)latDistance / 2.0) * Math.Sin((double)latDistance / 2.0)
              + Math.Cos(ToRadians(Latitude)) * Math.Cos(ToRadians(others.Latitude))
              * Math.Sin(lngDistance / 2) * Math.Sin(lngDistance / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return AVERAGE_RADIUS_OF_EARTH_KM * c;
        }

        private static double ToRadians(decimal x)
        {
            return (double)x * Math.PI / 180;
        }

        public bool IsNear(Coords others)
        {
            return IsCloser(others, Distances.NEAR);
        }

        public bool IsCloser(Coords others, Distances distance)
        {
            Console.WriteLine(HaversineDistanceKM(others));
            return (HaversineDistanceKM(others) <= (double)distance);
        }
    }
}
