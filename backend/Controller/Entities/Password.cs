using System.Text.RegularExpressions;

namespace backend.Controller.Entities
{
    public class Password
    {
        public string Value { get; set; }

        public Password(string value)
        {
            Value = value;
        }

        public static bool ValidateLength(string password)
        {
            return password.Length >= 8;
            
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

    }
}
