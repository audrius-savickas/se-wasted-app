using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.NewFolder
{
    public class InputValidator
    {
        public static bool ValidateSortOrder(string sortOrder, Coords userCoordinates = null)
        {
            if (userCoordinates == null && (sortOrder == "dist" || sortOrder == "dist_desc"))
            {
                throw new System.ArgumentNullException("Invalid user coordinates.");
            }

            return sortOrder switch
            {
                "dist" => true,
                "dist_desc" => true,
                "name" => true,
                "name_desc" => true,
                "price" => true,
                "price_desc" => true,
                "time" => true,
                "time_desc" => true,
                null => true,
                _ => throw new System.ArgumentException("Invalid sort order"),
            };
        }





    }
}
