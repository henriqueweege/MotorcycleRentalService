using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Application.Contracts.QueryHandlers
{
    public interface IMotorcycleQueryHandler
    {
        Task<QueryResponse<Motorcycle>> GetByPlate(string? plate);
        Task<QueryResponse<Motorcycle>> GetById(string id);
    }
}
