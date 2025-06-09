using MotorcycleRentalService.Domain.Contracts.RentalCalculationService;
using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Domain.Services.RentalCalculations
{
    public class RentalCalculationService : IRentalCalculationService
    {
        private readonly IEnumerable<IRentalCalculationServiceStrategy> _strategies;
        public RentalCalculationService(IEnumerable<IRentalCalculationServiceStrategy> strategies)
        {
            _strategies = strategies;
        }

        public decimal CalculateRentalTotalCost(Rental rental)
        {
            var strategy = _strategies.First(x => x.AppliesTo(rental));
            return strategy.CalculateRentalTotalCost(rental);
        }
    }
}
