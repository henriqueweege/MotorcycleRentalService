using MotorcycleRentalService.Application.Contracts.QueryHandlers;

namespace MotorcycleRentalService.Api.Extensions
{

    public static class QueryHandlersInjections
    {
        public static void AddQueryHandlers(this IServiceCollection services)
        {
            services.AddScoped<IMotorcycleQueryHandler, MotorcycleRentalService.Application.QueryHandlers.MotorcycleQueryHandler>();
            services.AddScoped<IRentalQueryHandler, MotorcycleRentalService.Application.QueryHandlers.RentalQueryHandler>();
        }
    }
}
