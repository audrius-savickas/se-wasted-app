using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wasted_app
{
    class Validator
    {
        public static String validatePassword(String password)
        {
            String error = "";
            if (password.Length < 8)
            {
                error += "• Password should be at least 8 characters long\n";
            }
            if (!password.Any(c => char.IsLetter(c)))
            {
                error += "• Password should contain at least 1 letter\n";
            }
            if (!password.Any(c => !char.IsLetterOrDigit(c) && !char.IsControl(c)))
            {
                error += "• Password should contain at least 1 special character\n";
            }
            if (!password.Any(c => char.IsDigit(c)))
            {
                error += "• Password should contain at least 1 digit";
            }
            return error;
        }
    }
}
