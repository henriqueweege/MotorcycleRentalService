using MotorcycleRentalService.Domain.Contracts.RentalCalculationService;
using MotorcycleRentalService.Domain.Services;
using MotorcycleRentalService.Domain.Services.Strategies;
using MotorcycleRentalService.Domain.Services.Strategies.PostDue;
using MotorcycleRentalService.Domain.Services.Strategies.PreDue;

namespace MotorcycleRentalService.Api.Extensions
{
    public static class RentalCalculationServiceInjections
    {

        public static void AddRentalCalculationService(this IServiceCollection services)
        {
            services.AddSingleton<IRentalCalculationServiceStrategy, PreDueRentalCalculationStrategy>();
            services.AddSingleton<IRentalCalculationServiceStrategy, PostDueRentalCalculationStrategy>();
            services.AddSingleton<IRentalCalculationServiceStrategy, DefaultRentalCalculation>();
            services.AddSingleton<IRentalCalculationService, RentalCalculationService>();
        }
    }
}
