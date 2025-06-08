using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Infrastructure.Repository.Contracts
{
    public interface IMotorcycleRepository : IDefaultRepository<Motorcycle>
    {
        bool ExistsAnyByPlate(string plate);
        Motorcycle? GetByPlate(string plate);
    }
}
