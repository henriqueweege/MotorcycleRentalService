using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Domain.Contracts.RentalCalculationService
{
    public interface IRentalCalculationService
    {
        decimal CalculateRentalTotalCost(Rental rental);
    }
}
