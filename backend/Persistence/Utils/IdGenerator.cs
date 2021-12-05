using System;

namespace Persistence.Utils
{
    public class IdGenerator
    {
        public static string GenerateUniqueId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
