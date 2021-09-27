using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using console_wasted_app.Controller.Entities;

namespace wasted_app
{
    class Validator
    {
        private static int minimumPasswordLength = 8;
        public static String validatePassword(String password)
        {
            string error = "";

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

        public static String validateEmail(String email)
        {
            if (!Mail.Validate(email))
            {
                return "• Invalid email\n";
            }
            return "";
        }
    }
}
