using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.QueryParameters
{
    public class FoodParameters : QueryStringParameters
    {
        public string SortOrder { get; set; } = "time_desc";
        public bool Reserved { get; set; } = false;
    }
}
