using MotorcycleRentalService.Application.Contracts.QueryHandlers;
using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Application.Responses.RentalQueryReponses;
using MotorcycleRentalService.Domain.Contracts.RentalCalculationService;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;

namespace MotorcycleRentalService.Application.QueryHandlers
{
    public class RentalQueryHandler : IRentalQueryHandler
    {
        private readonly IDefaultRepository<Rental> _repository;
        private readonly IRentalCalculationService _calculationService;
        public RentalQueryHandler(IDefaultRepository<Rental> repository, IRentalCalculationService calculationService)
        {
            _calculationService = calculationService;
            _repository = repository;
        }

        public Task<QueryResponse<Rental>> GetById(string id)
        {
            var response = new QueryResponse<Rental>();
            response.Success = false;
            try
            {
                var rental = _repository.GetById(id);
                if(rental is not null)
                {
                    response.Success = true;
                    response.Objects = new List<Rental> { rental };
                    return Task.FromResult(response);
                }
            }
            catch
            {
                response.Success = false;
            }

            return Task.FromResult(response);
        }

        public Task<QueryResponse<TotalRentalCostResponse>> GetTotalCost(string id)
        {
            var response = new QueryResponse<TotalRentalCostResponse>();
            response.Success = false;
            try
            {
                var rental = _repository.GetById(id);
                if(rental is not null)
                {
                    var totalCost = _calculationService.CalculateRentalTotalCost(rental);
                    response.Success = true;
                    response.Objects = new List<TotalRentalCostResponse>() { new () { TotalCost = totalCost } };
                }
            }
            catch
            {
                response.Success = false;
            }

            return Task.FromResult(response);
        }
    }
}
