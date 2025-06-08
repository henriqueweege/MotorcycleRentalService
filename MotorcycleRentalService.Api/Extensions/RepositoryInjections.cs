using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;
using MotorcycleRentalService.Infrastructure.Repository;

namespace MotorcycleRentalService.Api.Extensions
{
    public static class RepositoryInjections
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDefaultRepository<DeliveryMan>, DefaultRepository<DeliveryMan>>();
            services.AddScoped<IDefaultRepository<Rental>, DefaultRepository<Rental>>();
            services.AddScoped<IDefaultRepository<Motorcycle>, DefaultRepository<Motorcycle>>();
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
        }
    }
}
