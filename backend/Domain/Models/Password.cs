using System;
using System.Text.RegularExpressions;

namespace Domain.Models
{
    public class Password : IEquatable<Password>
    {
        private static readonly int minimumPasswordLength = 8;
        public string Value { get; set; }

        public Password() { }

        public Password(string value)
        {
            Value = value;
        }

        public static bool ValidateLength(string password)
        {
            return password.Length >= minimumPasswordLength;
        }

        public static bool ValidateLetter(string password)
        {
            var hasLetter = new Regex(@"[a-zA-Z]+");

            return hasLetter.IsMatch(password);
        }

        public static bool ValidateNumber(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");

            return hasNumber.IsMatch(password);
        }

        public static bool ValidateSpecial(string password)
        {
            var hasSpecialCharacter = new Regex(@"[!@#$%^&*_-]+");
            
            return hasSpecialCharacter.IsMatch(password);
        }

        public virtual bool Equals(Password other)
        {
            if (other == null) return false;
            return Value.CompareTo(other.Value) == 0;
        }
    }
}
