using MotorcycleRentalService.Application.Contracts.Services;
using MotorcycleRentalService.Application.Services;

namespace MotorcycleRentalService.Api.Extensions
{
    public static class ApplicationServicesInjections
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentSaver, DocumentSaver>();
        }
    }
}
