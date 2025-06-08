using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Domain.Contracts.RentalCalculationService
{
    public interface IRentalCalculationServiceStrategy
    {
        decimal CalculateRentalDailyCost(Rental rental);
        bool AppliesTo(Rental rental);
    }
}
