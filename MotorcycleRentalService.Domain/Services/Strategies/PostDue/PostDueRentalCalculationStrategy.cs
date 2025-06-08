using MotorcycleRentalService.Domain.Entities;


namespace MotorcycleRentalService.Domain.Services.Strategies.PostDue
{
    public class PostDueRentalCalculationStrategy : DefaultRentalCalculation
    {
        public override bool AppliesTo(Rental rental)
        {
            return rental.EndDate < rental.EffectiveEndDate;
        }

        public override decimal CalculateRentalDailyCost(Rental rental)
        {
            var baseCalculation = base.CalculateRentalDailyCost(rental);
            var totalOverdueDays = rental.GetTotalOverdueDays();

            return baseCalculation + totalOverdueDays * 50;
        }
    }
}
