using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Infrastructure.Repository.Contracts
{
    public interface IRentalRepository : IDefaultRepository<Rental>
    {
        bool AnyValidRentalWithMorotcycle(string motorcycleId, DateTime effectiveEndDate);
    }
}
