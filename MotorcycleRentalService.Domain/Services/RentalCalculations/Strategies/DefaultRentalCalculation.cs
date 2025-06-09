using MotorcycleRentalService.Domain.Contracts.RentalCalculationService;
using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Domain.Services.RentalCalculations.Strategies
{
    public class DefaultRentalCalculation : IRentalCalculationServiceStrategy
    {
        public virtual bool AppliesTo(Rental rental)
        {
            return (rental.EffectiveEndDate.Date - rental.EndDate.Date).Days == 0;
        }

        public virtual decimal CalculateRentalTotalCost(Rental rental)
         => rental.GetTotalRentalDays() * rental.GetDailyFee();
    }
}
