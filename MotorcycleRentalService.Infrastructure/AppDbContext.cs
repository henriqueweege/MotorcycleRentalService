
using Microsoft.EntityFrameworkCore;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Domain.Events;
using MotorcycleRentalService.Infrastructure.Mappings;

namespace MotorcycleRentalService.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Motorcycle> Motorcycle { get; set; }
        public DbSet<DeliveryMan> DeliveryMen { get; set; }
        public DbSet<Rental> Rental { get; set; }
        public DbSet<MotorcycleCreated> MotorcycleCreated { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MotorcycleMappings());
            modelBuilder.ApplyConfiguration(new DeliveryManMappings());
            modelBuilder.ApplyConfiguration(new RentalMappings());
            modelBuilder.ApplyConfiguration(new MotorcycleCreatedMappings());

            base.OnModelCreating(modelBuilder);
        }
    }
}
