using FluentAssertions;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Domain.Services.RentalCalculations.Strategies.PreDue;

namespace MotorcycleRentalService.UnitTests.Domain.Services.RentalCalculations
{
    public class PreDueRentalCalculationStrategyTests
    {
        private readonly PreDueRentalCalculationStrategy _strategy = new PreDueRentalCalculationStrategy();

        [Fact]
        public void GivenPreDueRental_AppliesTo_ShouldReturnTrue()
        {
            //Arrange
            var rental = new Rental();
            rental.EndDate = DateTime.UtcNow;
            rental.EffectiveEndDate = rental.EndDate.AddDays(-1);

            //Act
            var appliesTo = _strategy.AppliesTo(rental);

            //Assert
            appliesTo.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void GivenPreDueRental_AppliesTo_ShouldReturnFalse(DateTime endDate, DateTime effectiveEndDate)
        {
            //Arrange
            var rental = new Rental();
            rental.EffectiveEndDate = effectiveEndDate;
            rental.EndDate = endDate;

            //Act
            var appliesTo = _strategy.AppliesTo(rental);

            //Assert
            appliesTo.Should().BeFalse();
        }

        [Theory]
        [InlineData(7, 216)]
        [InlineData(15, 548.8)]
        public void GivenPreDueRental_CalculateRentalTotalCost_ShouldCalculateCorrectly(int maxDays, decimal expectedRentalCost)
        {
            //Arrange
            var rental = new Rental();
            rental.StartDate = DateTime.UtcNow;
            rental.EndDate = rental.StartDate.AddDays(maxDays);
            rental.EffectiveEndDate = rental.EndDate.AddDays(-1);
            rental.MaxDays = maxDays;

            //Act
            var rentalCost = _strategy.CalculateRentalTotalCost(rental);

            //Assert
            rentalCost.Should().Be(expectedRentalCost);
        }

        public static IEnumerable<object[]> Data =>
               new List<object[]>
               {
            new object[] { DateTime.UtcNow, DateTime.UtcNow},
            new object[] { DateTime.UtcNow, DateTime.UtcNow.AddDays(1) }
               };
    }
}
