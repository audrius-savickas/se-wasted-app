﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TypeOfFoodEntity : Entity
    {
        public string Name { get; set; }

        public virtual TypeOfFoodEntity TypesOfFood { get; set; }
    }
}
