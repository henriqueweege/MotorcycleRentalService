using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Infrastructure.Repository.Contracts
{
    public interface IMotorcycleRepository : IDefaultRepository<Motorcycle>
    {
        Task<bool> ExistsAnyByPlate(string plate);
        Task<Motorcycle> GetByPlate(string plate);
    }
}
