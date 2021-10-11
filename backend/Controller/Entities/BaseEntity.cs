namespace wasted_app.Controller.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public BaseEntity () { }

        public BaseEntity(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
