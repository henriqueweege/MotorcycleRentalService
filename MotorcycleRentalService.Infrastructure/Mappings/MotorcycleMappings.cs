using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Infrastructure.Mappings
{
     
    internal class MotorcycleMappings : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.ToTable("motorcycle");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Model).HasColumnName("model");
            builder.Property(x => x.Year).HasColumnName("year");
            builder.Property(x => x.Plate).HasColumnName("plate");
        }
    }
}
