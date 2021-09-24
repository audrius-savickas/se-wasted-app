using System;
using System.Text.Json;
using wasted_app.Controller.Entities;

namespace console_wasted_app.Controller.Entities
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
            Coords = new Coords(
                json.GetProperty("Latitude").GetDecimal(),
                json.GetProperty("Altitude").GetDecimal()
            );
            Credentials = new Credentials(
                json.GetProperty("Mail").GetString(),
                json.GetProperty("Password").GetString()
            );
        }
    }
}
