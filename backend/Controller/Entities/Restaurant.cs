using backend.Controller.Helpers;
using System.Text.Json;
using wasted_app.Controller.Entities;

namespace backend.Controller.Entities
{
    public class Restaurant : BaseEntity
    {
        public Coords Coords { get; set; }
        public Credentials Credentials { get; set; }

        public Restaurant(string id, string name, Coords coords, Credentials creds)
            : base(id, name)
        {
            Coords = coords;
            Credentials = creds;
        }

        public Restaurant(JsonElement json) : base(json)
        {
            JsonElement coordsJson = json.GetProperty("Coords");
            Coords = new Coords(
                coordsJson.GetProperty("Longitude").GetDecimal(),
                coordsJson.GetProperty("Latitude").GetDecimal()
            );

            JsonElement credsJson = json.GetProperty("Credentials");
            JsonElement mailJson = credsJson.GetProperty("Mail");
            JsonElement passwordJson = credsJson.GetProperty("Password");

            Credentials = new Credentials(
                mailJson.GetProperty("Value").GetString(),
                passwordJson.GetProperty("Value").GetString()
            );
        }

        public bool IsNear(Coords coords)
        {
            return IsCloser(coords, Distances.NEAR);
        }

        public bool IsCloser(Coords coords, Distances distance)
        {
            return CoordsHelper.IsCloser(Coords, coords, distance);
        }
    }
}
