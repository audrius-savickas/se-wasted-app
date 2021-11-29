﻿using Domain.Models;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class TypeOfFoodRepository : BaseRepository<TypeOfFood>, ITypeOfFoodRepository
    {
        public TypeOfFoodRepository(string pathToDatabase) : base(pathToDatabase)
        {
        }
    }
}
