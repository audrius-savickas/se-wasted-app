using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<TypeOfFood> TypesOfFood { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>(x =>
            {
                x.HasKey(k => k.Id);
                x.Property(p => p.Name).IsRequired();


                // TODO
                x.Ignore(p => p.Coords);
                x.Ignore(p => p.Credentials);


                x.Property(p => p.Address).IsRequired();
            }).Entity<Food>(x =>
            {
                x.HasKey(k => k.Id);
                x.Property(p => p.Name).IsRequired();
            }).Entity<TypeOfFood>(x =>
            {
                x.HasKey(k => k.Id);
                x.Property(p => p.Name).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
