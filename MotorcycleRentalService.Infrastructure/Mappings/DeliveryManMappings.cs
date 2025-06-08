using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Infrastructure.Mappings
{

    internal class DeliveryManMappings : IEntityTypeConfiguration<DeliveryMan>
    {
        public void Configure(EntityTypeBuilder<DeliveryMan> builder)
        {
            builder.ToTable("delivery_man");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Birthday).HasColumnName("birthday");
            builder.Property(x => x.Cnpj).HasColumnName("cnpj");
            builder.Property(x => x.DriverLicenseType).HasColumnName("driver_license_type");
            builder.Property(x => x.DriverLicenseNumber).HasColumnName("driver_license_number");
        }
    }
}
