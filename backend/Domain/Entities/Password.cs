using System;
using System.Text.RegularExpressions;

namespace Domain.Entities
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

            if (!hasLetter.IsMatch(password))
            {
                return false;
            }
            return true;
        }

        public static bool ValidateNumber(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");

            if (!hasNumber.IsMatch(password))
            {
                return false;
            }
            return true;
        }

        public static bool ValidateSpecial(string password)
        {
            var hasSpecialCharacter = new Regex(@"[!@#$%^&*_-]+");

            if (!hasSpecialCharacter.IsMatch(password))
            {
                return false;
            }
            return true;
        }

        public virtual bool Equals(Password other)
        {
            if (other == null) return false;
            return Value.CompareTo(other.Value) == 0;
        }
    }
}
