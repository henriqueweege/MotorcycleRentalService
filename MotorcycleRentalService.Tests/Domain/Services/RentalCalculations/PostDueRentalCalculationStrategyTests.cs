using FluentAssertions;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Domain.Services.RentalCalculations.Strategies.PostDue;

namespace MotorcycleRentalService.UnitTests.Domain.Services.RentalCalculations
{
    public class PostDueRentalCalculationStrategyTests
    {
        private readonly PostDueRentalCalculationStrategy _strategy = new PostDueRentalCalculationStrategy();

        [Fact]
        public void GivenPostDueRental_AppliesTo_ShouldReturnTrue()
        {
            //Arrange
            var rental = new Rental();
            rental.EffectiveEndDate = DateTime.UtcNow;
            rental.EndDate = rental.EffectiveEndDate.AddDays(-1);

            //Act
            var appliesTo = _strategy.AppliesTo(rental);

            //Assert
            appliesTo.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void GivenPostDueRental_AppliesTo_ShouldReturnFalse(DateTime effectiveEndDate, DateTime endDate)
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

        [Fact]
        public void GivenPostDueRental_CalculateRentalTotalCost_ShouldCalculateCorrectly()
        {
            //Arrange
            var rental = new Rental();
            rental.EffectiveEndDate = DateTime.UtcNow;
            rental.EndDate = rental.EffectiveEndDate.AddDays(-1);
            rental.StartDate = rental.EndDate.AddDays(-7);
            rental.MaxDays = 7;

            //Act
            var rentalCost = _strategy.CalculateRentalTotalCost(rental);

            //Assert
            rentalCost.Should().Be(290m);
        }

        public static IEnumerable<object[]> Data =>
               new List<object[]>
               {
            new object[] { DateTime.UtcNow, DateTime.UtcNow},
            new object[] { DateTime.UtcNow, DateTime.UtcNow.AddDays(1) }
               };
    }
}
