using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Domain.Contracts.RentalCalculationService
{
    public interface IRentalCalculationServiceStrategy
    {
        decimal CalculateRentalTotalCost(Rental rental);
        bool AppliesTo(Rental rental);
    }
}
