using MotorcycleRentalService.Domain.Entities;


namespace MotorcycleRentalService.Domain.Services.Strategies.PreDue
{
    public class PreDueRentalCalculationStrategy : DefaultRentalCalculation
    {
        public override bool AppliesTo(Rental rental)
        {
            return rental.EndDate > rental.EffectiveEndDate;
        }

        public override decimal CalculateRentalDailyCost(Rental rental)
        {
            var baseCalculation = base.CalculateRentalDailyCost(rental);

            switch (rental.MaxDays)
            {
                case 7:
                    return baseCalculation + (baseCalculation * 0.2m);
                case 15:
                    return baseCalculation + (baseCalculation * 0.4m);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
