using System;
namespace Services.Options
{
    public class EmailOptions
    {
        public String Host { get; set; }
        public String Password { get; set; }
        public String UserName { get; set; }
        public int Port { get; set; }
    }
}
