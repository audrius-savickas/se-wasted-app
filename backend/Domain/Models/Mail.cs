using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Mail : IEquatable<Mail>
    {
        public string Value { get; set; }

        public Mail() { }

        public Mail(string value)
        {
            Value = value;
        }

        public static bool Validate(string email)
        {
            return email != null && new EmailAddressAttribute().IsValid(email);
        }

        public virtual bool Equals(Mail other)
        {
            if (other == null) return false;
            return Value.CompareTo(other.Value) == 0;
        }
    }
}
