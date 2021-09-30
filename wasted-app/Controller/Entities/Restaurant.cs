﻿using System.Text.Json;
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
            var coordsJson = json.GetProperty("Coords");
            Coords = new Coords(
                coordsJson.GetProperty("Longitude").GetDecimal(),
                coordsJson.GetProperty("Latitude").GetDecimal()
            );

            var credsJson = json.GetProperty("Credentials");
            var mailJson = credsJson.GetProperty("Mail");
            var passwordJson = credsJson.GetProperty("Password");

            Credentials = new Credentials(
                mailJson.GetProperty("Value").GetString(),
                passwordJson.GetProperty("Value").GetString()
            );
        }

        public bool IsNear(Coords coords)
        {
            return Coords.IsNear(coords);
        }
    }
}
