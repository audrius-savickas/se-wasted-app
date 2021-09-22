using System.Text.Json;

namespace wasted_app.Controller.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public BaseEntity(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public BaseEntity(JsonElement json)
        {
            Id = json.GetProperty("id").GetString();
            Name = json.GetProperty("name").GetString();
        }
    }
}
