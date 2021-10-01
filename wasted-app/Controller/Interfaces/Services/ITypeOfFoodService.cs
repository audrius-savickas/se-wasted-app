﻿using console_wasted_app.Controller.Entities;
using System.Collections.Generic;

namespace console_wasted_app.Controller.Interfaces
{
    public interface ITypeOfFoodService
    {
        TypeOfFood GetByTypeOfFoodId(string id);
        IEnumerable<TypeOfFood> GetAllTypesOfFood();
        void AddTypeOfFood(TypeOfFood newTypeOfFood);
        void UpdateTypeOfFood(TypeOfFood updatedTypeOfFood);
        void DeleteTypeOfFood(string id);
    }
}
