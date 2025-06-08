using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;

namespace MotorcycleRentalService.Infrastructure.Repository
{
    public class RentalRepository : DefaultRepository<Rental>, IRentalRepository
    {
        public RentalRepository(AppDbContext context) : base(context)
        {
        }

        public bool AnyValidRentalWithMorotcycle(string motorcycleId, DateTime effectiveEndDate)
        {
            return EntitySet.Any(x => x.MotorcycleId == motorcycleId && (x.StartDate <= effectiveEndDate && x.EffectiveEndDate >= effectiveEndDate));
        }
    }
}
