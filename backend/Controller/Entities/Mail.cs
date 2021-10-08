using System.ComponentModel.DataAnnotations;

namespace console_wasted_app.Controller.Entities
{
    public class Mail
    {
        public string Value { get; set; }

        public Mail(string value)
        {
            Value = value;
        }

        public static bool Validate(string email)
        {
            if (email == null || !(new EmailAddressAttribute().IsValid(email)))
            {
                return false;
            }
            return true;
        }
    }
}
