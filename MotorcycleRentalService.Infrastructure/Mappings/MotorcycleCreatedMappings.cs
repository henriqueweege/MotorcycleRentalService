using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MotorcycleRentalService.Domain.Events;

namespace MotorcycleRentalService.Infrastructure.Mappings
{
    public class MotorcycleCreatedMappings : IEntityTypeConfiguration<MotorcycleCreated>
    {
        public void Configure(EntityTypeBuilder<MotorcycleCreated> builder)
        {
            builder.ToTable("motorcycle_created");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.MotorcycleYear).HasColumnName("motorcycle_year");
        }
    }
}
