namespace Domain.Entities
{
    public class Coords
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public Coords(decimal longitude, decimal latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
