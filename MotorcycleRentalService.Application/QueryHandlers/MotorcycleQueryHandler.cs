using MotorcycleRentalService.Application.Contracts.QueryHandlers;
using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;
using Serilog;

namespace MotorcycleRentalService.Application.QueryHandlers
{
    public class MotorcycleQueryHandler : IMotorcycleQueryHandler
    {
        private readonly IMotorcycleRepository _repository;
        public MotorcycleQueryHandler(IMotorcycleRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueryResponse<Motorcycle>> GetById(string id)
        {
            var response = new QueryResponse<Motorcycle>();

            try
            {
                var motorcycle = _repository.GetById(id);

                response.Success = motorcycle is not null;
                response.Objects = response.Success ? new List<Motorcycle>() { motorcycle } : new List<Motorcycle>();
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                response.Success = false;
            }

            return await Task.FromResult(response);
        }

        public async Task<QueryResponse<Motorcycle>> GetByPlate(string? plate)
        {
            var response = new QueryResponse<Motorcycle>();

            try
            {
                if (string.IsNullOrEmpty(plate))
                {

                    response.Objects = await _repository.GetAll();
                    response.Success = response.Objects.Any();
                    return response;
                }

                var motorcycle = _repository.GetByPlate(plate);

                response.Success = motorcycle is not null;
                response.Objects = response.Success ? new List<Motorcycle>() { motorcycle } : new List<Motorcycle>();
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                response.Success = false;
            }

            return response;
        }
    }
}
