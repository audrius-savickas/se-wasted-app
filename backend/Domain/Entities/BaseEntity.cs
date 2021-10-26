using System;

﻿namespace Domain.Entities
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public BaseEntity () { }

        public BaseEntity(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool Equals(BaseEntity other)
        {
            if (other == null) return false;
            return (Id.CompareTo(other.Id) == 0);
        }
    }
}
