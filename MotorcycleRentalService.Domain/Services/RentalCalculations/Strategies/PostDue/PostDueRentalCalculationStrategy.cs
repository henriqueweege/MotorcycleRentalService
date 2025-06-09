using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Domain.Services.RentalCalculations.Strategies;


namespace MotorcycleRentalService.Domain.Services.RentalCalculations.Strategies.PostDue
{
    public class PostDueRentalCalculationStrategy : DefaultRentalCalculation
    {
        public override bool AppliesTo(Rental rental)
        {
            return rental.EndDate < rental.EffectiveEndDate;
        }

        public override decimal CalculateRentalTotalCost(Rental rental)
        {
            var baseCalculation = base.CalculateRentalTotalCost(rental);
            var totalOverdueDays = rental.GetTotalOverdueDays();

            return baseCalculation + totalOverdueDays * 50;
        }
    }
}
