using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Infrastructure.Mappings
{

    internal class RentalMappings : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("rental");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.MaxDays).HasColumnName("max_days");
            builder.Property(x => x.StartDate).HasColumnName("start_date");
            builder.Property(x => x.EndDate).HasColumnName("end_date");
            builder.Property(x => x.MaxDays).HasColumnName("max_days");
            builder.Property(x => x.EffectiveEndDate).HasColumnName("effective_end_date");
            builder.Property(x => x.MotorcycleId).HasColumnName("motorcycle_id");
            builder.Property(x => x.DeliveryManId).HasColumnName("delivery_man_id");
        }
    }
}
