using Domain.Models;
using System;

namespace Domain.Helpers
{
    public static class CoordsHelper
    {
        private const double AVERAGE_RADIUS_OF_EARTH_KM = 6371;


        public static double HaversineDistanceKM(Coords one, Coords others)
        {
            _ = one ?? throw new ArgumentNullException(nameof(one));
            _ = others ?? throw new ArgumentNullException(nameof(others));

            double latDistance = ToRadians(one.Latitude - others.Latitude);
            double lngDistance = ToRadians(one.Longitude - others.Longitude);

            double a =
                Math.Sin((double)latDistance / 2.0) * Math.Sin((double)latDistance / 2.0)
                + Math.Cos(ToRadians(one.Latitude)) * Math.Cos(ToRadians(others.Latitude))
                * Math.Sin(lngDistance / 2) * Math.Sin(lngDistance / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return AVERAGE_RADIUS_OF_EARTH_KM * c;
        }

        private static double ToRadians(decimal x)
        {
            return (double)x * Math.PI / 180;
        }

        public static bool IsNear(Coords one, Coords others)
        {
            return IsCloser(one, others, Distances.NEAR);
        }

        public static bool IsCloser(Coords one, Coords others, Distances distance)
        {
            return (HaversineDistanceKM(one, others) <= (double)distance);
        }
    }
}
