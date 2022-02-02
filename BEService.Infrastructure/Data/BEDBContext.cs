using BEService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BEService.Infrastructure.Data
{
    public class BEDBContext : DbContext
    {
        public BEDBContext(DbContextOptions<BEDBContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MenuApp> MenuApps { get; set; }
        public DbSet<MenuAccess> MenuAccesss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CreatedAt indentities
            foreach(var entity in modelBuilder.Model.GetEntityTypes()
                .Where(x =>
                    x.ClrType.GetProperties().Any(y => 
                        y.CustomAttributes.Any(z => 
                            z.AttributeType == typeof(DatabaseGeneratedAttribute)))))
            {
                foreach(var property in entity.ClrType.GetProperties()
                    .Where(x =>
                        x.PropertyType == typeof(DateTime) && x.CustomAttributes.Any(y =>
                            y.AttributeType == typeof(DatabaseGeneratedAttribute))))
                {
                    modelBuilder
                        .Entity(entity.ClrType)
                        .Property(property.Name)
                        .HasDefaultValueSql("GETUTCDATE()");
                }
            }

            // IsDelete identities
            foreach (var entity in modelBuilder.Model.GetEntityTypes()
                .Where(t =>
                    t.ClrType.GetProperties()
                        .Any(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(DatabaseGeneratedAttribute)))))
            {
                foreach (var property in entity.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(bool?) && p.CustomAttributes.Any(a => a.AttributeType == typeof(DatabaseGeneratedAttribute))))
                {
                    modelBuilder
                        .Entity(entity.ClrType)
                        .Property(property.Name)
                        .HasDefaultValue(false);
                }
            }
        }
    }
}
