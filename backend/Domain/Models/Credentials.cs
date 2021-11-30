using System;

namespace Domain.Models
{
    public class Credentials : IEquatable<Credentials>
    {
        public Mail Mail { get; set; }
        public Password Password { get; set; }

        public Credentials() { }

        public Credentials(string mail, string password)
        {
            Mail = new Mail(mail);
            Password = new Password(password);
        }

        public virtual bool Equals(Credentials other)
        {
            if (other == null) return false;
            return Mail.Equals(other.Mail) && Password.Equals(other.Password);
        }
    }
}
