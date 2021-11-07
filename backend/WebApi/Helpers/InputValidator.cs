using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.NewFolder
{
    public class InputValidator
    {
        public bool ValidateSortOrder(string sortOrder, decimal? latitude, decimal? longitude)
        {
            if ((latitude == null || longitude == null) && (sortOrder == "dist" || sortOrder == "dist_desc"))
            {
                throw new System.ArgumentNullException("Invalid user coordinates.");
            }

            return sortOrder switch
            {
                "dist" => true,
                "dist_desc" => true,
                "name" => true,
                "name_desc" => true,
                _ => throw new System.ArgumentException("Invalid sort order"),
            };
        }





    }
}
