using System;
namespace console_wasted_app.Controller.Entities
{
    public struct Credentials
    {
        public Mail Mail { get; set; }
        public Password Password { get; set; }

        public Credentials( string mail, string password )
        {
            Mail = new Mail(mail);
            Password = new Password(password);
        }
    }
}
