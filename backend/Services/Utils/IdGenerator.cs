using System;

namespace Services.Utils
{
    public class IdGenerator
    {
        public static string GenerateUniqueId()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
        }
    }
}
