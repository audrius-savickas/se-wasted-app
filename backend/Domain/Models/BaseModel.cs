using System;

namespace Domain.Models
{
    public abstract class BaseModel : IEquatable<BaseModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public BaseModel() { }

        public BaseModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool Equals(BaseModel other)
        {
            if (other == null) return false;
            return (Id.CompareTo(other.Id) == 0);
        }
    }
}
