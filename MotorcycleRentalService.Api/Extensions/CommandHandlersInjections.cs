using MotorcycleRentalService.Application.Contracts.CommandHandlers;

namespace MotorcycleRentalService.Api.Extensions
{
    public static class CommandHandlersInjections
    {
        public static void AddCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<IMotorcycleCommandHandler, MotorcycleRentalService.Application.CommandHandlers.MotorcycleHandler>();
            services.AddScoped<IDeliveryManCommandHandler, MotorcycleRentalService.Application.CommandHandlers.DeliveryManHandler>();
            services.AddScoped<IRentalCommandHandler, MotorcycleRentalService.Application.CommandHandlers.RentalHandler>();
        }
    }
}
