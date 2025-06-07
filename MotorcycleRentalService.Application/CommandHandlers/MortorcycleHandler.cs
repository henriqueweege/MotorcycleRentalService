using MotorcycleRentalService.Application.Commands.MotorcycleCommands;
using MotorcycleRentalService.Application.Contracts.CommandHandlers;
using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;

namespace MotorcycleRentalService.Application.CommandHandlers
{
    internal class MortorcycleHandler : IMotorcycleCommandHandler
    {
        private readonly IMotorcycleRepository _repository;
        public MortorcycleHandler(IMotorcycleRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResponse> Handle(Create command)
        {
            var response = new CommandResponse();
            
            try
            {
                await _repository.Add(command.Motorcycle);
                response.Success = true;
            }
            catch
            {
                response.Success = false;
            }

            return await Task.FromResult(response);
        }

        public async Task<CommandResponse> Handle(Update command)
        {
            var response = new CommandResponse();

            try
            {
                response.Success = false;

                var motorcycle = await _repository.GetById(command.Id);
                
                if(motorcycle is not null && (!await _repository.ExistsAnyByPlate(command.Plate) || motorcycle.Plate == command.Plate))
                {
                    motorcycle.Plate = command.Plate;
                    await _repository.Update(motorcycle);
                    response.Success = true;
                }                
            }
            catch
            {
                response.Success = false;
            }

            return await Task.FromResult(response);
        }

        public async Task<CommandResponse> Handle(Delete command)
        {
            var response = new CommandResponse();

            try
            {
                var motorcycle = await _repository.GetById(command.Id);
                await _repository.Delete(motorcycle);
                response.Success = true;
            }
            catch
            {
                response.Success = false;
            }

            return await Task.FromResult(response);
        }
    }
}
