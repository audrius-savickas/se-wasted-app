using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public interface IDatabaseContext
    {
        DbSet<Restaurant> Restaurants { get; set; }
        DbSet<Food> Foods { get; set; }
        DbSet<TypeOfFood> TypesOfFood { get; set; }
        int SaveChanges();
    }
}
