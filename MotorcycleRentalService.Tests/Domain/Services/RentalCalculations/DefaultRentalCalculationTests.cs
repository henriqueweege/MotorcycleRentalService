
using FluentAssertions;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Domain.Services.RentalCalculations.Strategies;

namespace MotorcycleRentalService.UnitTests.Domain.Services.RentalCalculations
{
    public class DefaultRentalCalculationTests
    {
        private readonly DefaultRentalCalculation _defaultRentalCalculation = new DefaultRentalCalculation();

        [Fact]
        public void GivenRentalEndingInExpectedDate_AppliesTo_ShouldReturnTrue()
        {
            //Arrange
            var rental = new Rental();
            rental.EndDate = DateTime.UtcNow;
            rental.EffectiveEndDate = DateTime.UtcNow;
            rental.StartDate = DateTime.UtcNow;

            //Act
            var appliesTo = _defaultRentalCalculation.AppliesTo(rental);

            //Assert
            appliesTo.Should().BeTrue();
        }

        [Fact]
        public void GivenRentalEndingAfterExpectedDate_AppliesTo_ShouldReturnFalse()
        {
            //Arrange
            var rental = new Rental();
            rental.EndDate = DateTime.UtcNow;
            rental.EffectiveEndDate = DateTime.UtcNow.AddDays(-1);
            rental.StartDate = DateTime.UtcNow;

            //Act
            var appliesTo = _defaultRentalCalculation.AppliesTo(rental);

            //Assert
            appliesTo.Should().BeFalse();
        }

        [Fact]
        public void GivenRentalEndingBeforeExpectedDate_AppliesTo_ShouldReturnFalse()
        {
            //Arrange
            var rental = new Rental();
            rental.EndDate = DateTime.UtcNow;
            rental.EffectiveEndDate = DateTime.UtcNow.AddDays(-1);
            rental.StartDate = DateTime.UtcNow;

            //Act
            var appliesTo = _defaultRentalCalculation.AppliesTo(rental);

            //Assert
            appliesTo.Should().BeFalse();
        }

        [Theory]
        [InlineData(7, 210)]
        [InlineData(15, 420)]
        [InlineData(30, 660)]
        [InlineData(45, 900)]
        [InlineData(50, 900)]
        public void GivenValidRental_CalculateRentalTotalCost_ShouldReturnCorrectly(int maxDays, decimal expectedRentalCost)
        {
            //Arrange
            var rental = new Rental();
            rental.StartDate = DateTime.UtcNow;
            rental.EndDate = rental.StartDate.AddDays(maxDays);
            rental.EffectiveEndDate = rental.EndDate;
            rental.MaxDays = maxDays;

            //Act
            var rentalCost = _defaultRentalCalculation.CalculateRentalTotalCost(rental);

            //Assert
            rentalCost.Should().Be(expectedRentalCost);
        }
    }
}
