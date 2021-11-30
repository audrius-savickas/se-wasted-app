using System;

namespace Services.Utils
{
    public class IdGenerator
    {
        public static string GenerateUniqueId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
