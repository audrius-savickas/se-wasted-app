﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EF
{
    public interface IDatabaseContext
    {
        DbSet<RestaurantEntity> Restaurants { get; set; }
        DbSet<FoodEntity> Foods { get; set; }
        DbSet<TypeOfFoodEntity> TypesOfFood { get; set; }
        int SaveChanges();
    }
}