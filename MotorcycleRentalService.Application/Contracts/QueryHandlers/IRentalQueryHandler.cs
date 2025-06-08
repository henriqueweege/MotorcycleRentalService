using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Application.Responses.RentalQueryReponses;
using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Application.Contracts.QueryHandlers
{
    public interface IRentalQueryHandler
    {
        Task<QueryResponse<Rental>> GetById(string id);
        Task<QueryResponse<TotalRentalCostResponse>> GetTotalCost(string id);
    }
}
