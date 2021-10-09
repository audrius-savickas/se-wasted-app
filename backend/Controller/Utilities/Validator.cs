using backend.Controller.Entities;

namespace backend.Controller.Utilities
{
    internal class Validator
    {
        private static readonly int minimumPasswordLength = 8;
        public static string ValidatePassword(string password)
        {
            var error = "";

            if (!Password.ValidateLength(password))
            {
                error += "• Password should be at least 8 characters long\n";
            }
            if (!Password.ValidateLetter(password))
            {
                error += "• Password should contain at least 1 letter\n";
            }
            if (!Password.ValidateSpecial(password))
            {
                error += "• Password should contain at least 1 special character\n";
            }
            if (!Password.ValidateNumber(password))
            {
                error += "• Password should contain at least 1 digit";
            }

            return error;
        }

        public static string ValidateEmail(string email)
        {
            if (!Mail.Validate(email))
            {
                return "• Invalid email\n";
            }
            return "";
        }
    }
}
