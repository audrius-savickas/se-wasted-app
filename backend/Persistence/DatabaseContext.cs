using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<RestaurantEntity> Restaurants { get; set; }
        public DbSet<FoodEntity> Foods { get; set; }
        public DbSet<TypeOfFoodEntity> TypesOfFood { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.Tracked += OnEntityTracked;
            ChangeTracker.StateChanged += OnEntityStateChanged;
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestaurantEntity>(x =>
            {
                x.HasKey(k => k.Id);
                x.Property(p => p.Name).IsRequired();
                x.Property(p => p.Address).IsRequired();
                x.Property(p => p.Mail).IsRequired();
                x.Property(p => p.Password).IsRequired();
            });

            modelBuilder.Entity<FoodEntity>(x =>
            {
                x.HasKey(k => k.Id);
                x.HasOne(p => p.Restaurant).WithMany(p => p.Foods);
                x.HasMany(p => p.TypesOfFood).WithMany(p => Foods);
                x.Property(p => p.RestaurantId).IsRequired();
                x.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<TypeOfFoodEntity>(x =>
            {
                x.HasKey(k => k.Id);
                x.Property(p => p.Name).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        private void OnEntityTracked(object sender, EntityTrackedEventArgs e)
        {
            if (!e.FromQuery && e.Entry.State == EntityState.Added && e.Entry.Entity is Entity entity)
            {
                entity.CreatedOn = DateTime.UtcNow;
            }
        }

        private void OnEntityStateChanged(object sender, EntityStateChangedEventArgs e)
        {
            if (e.NewState == EntityState.Modified && e.Entry.Entity is Entity entity)
            {
                entity.ModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
