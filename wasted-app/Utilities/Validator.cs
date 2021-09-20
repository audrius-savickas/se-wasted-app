using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace wasted_app
{
    class Validator
    {
        public static String validatePassword(String password)
        {
            String error = "";
            var hasNumber = new Regex(@"[0-9]+");
            var hasLetter = new Regex(@"[a-zA-Z]+");
            var hasSpecialCharacter = new Regex(@"[!@#$%^&*_-]+");


            if (password.Length < 8)
            {
                error += "• Password should be at least 8 characters long\n";
            }
            if (!hasLetter.IsMatch(password))
            {
                error += "• Password should contain at least 1 letter\n";
            }
            if (!hasSpecialCharacter.IsMatch(password))
            {
                error += "• Password should contain at least 1 special character\n";
            }
            if (!hasNumber.IsMatch(password))
            {
                error += "• Password should contain at least 1 digit";
            }
            return error;
        }
    }
}
