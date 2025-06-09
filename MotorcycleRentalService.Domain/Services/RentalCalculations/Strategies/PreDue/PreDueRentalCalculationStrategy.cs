using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Domain.Services.RentalCalculations.Strategies;


namespace MotorcycleRentalService.Domain.Services.RentalCalculations.Strategies.PreDue
{
    public class PreDueRentalCalculationStrategy : DefaultRentalCalculation
    {
        public override bool AppliesTo(Rental rental)
        {
            return rental.EndDate > rental.EffectiveEndDate;
        }

        public override decimal CalculateRentalTotalCost(Rental rental)
        {
            var baseCalculation = base.CalculateRentalTotalCost(rental);

            switch (rental.MaxDays)
            {
                case 7:
                    return baseCalculation + baseCalculation * 0.2m;
                case 15:
                    return baseCalculation + baseCalculation * 0.4m;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
