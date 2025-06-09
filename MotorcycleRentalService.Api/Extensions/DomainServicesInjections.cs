using MotorcycleRentalService.Domain.Contracts.RentalCalculationService;
using MotorcycleRentalService.Domain.Services;
using MotorcycleRentalService.Domain.Services.Strategies;
using MotorcycleRentalService.Domain.Services.Strategies.PostDue;
using MotorcycleRentalService.Domain.Services.Strategies.PreDue;

namespace MotorcycleRentalService.Api.Extensions
{
    public static class DomainServicesInjections
    {

        public static void AddDomainService(this IServiceCollection services)
        {
            services.AddSingleton<IRentalCalculationServiceStrategy, PreDueRentalCalculationStrategy>();
            services.AddSingleton<IRentalCalculationServiceStrategy, PostDueRentalCalculationStrategy>();
            services.AddSingleton<IRentalCalculationServiceStrategy, DefaultRentalCalculation>();
            services.AddSingleton<IRentalCalculationService, RentalCalculationService>();
        }
    }
}
