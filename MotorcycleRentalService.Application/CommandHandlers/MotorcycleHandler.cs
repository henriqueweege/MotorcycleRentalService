using MotorcycleRentalService.Application.Commands.MotorcycleCommands;
using MotorcycleRentalService.Application.Contracts.CommandHandlers;
using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;

namespace MotorcycleRentalService.Application.CommandHandlers
{
    public class MotorcycleHandler : IMotorcycleCommandHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;
        public MotorcycleHandler(IMotorcycleRepository motorcycleRepository, IRentalRepository rentalRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<CommandResponse> Handle(Create command)
        {
            var response = new CommandResponse();
            
            try
            {
                var motorcycle = new Motorcycle()
                {
                    Id = command.Id,
                    Model = command.Model,
                    Plate = command.Plate,
                    Year = command.Year
                };

                await _motorcycleRepository.Add(motorcycle);
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

                var motorcycle = _motorcycleRepository.GetById(command.Id);
                
                if(motorcycle is not null && (!_motorcycleRepository.ExistsAnyByPlate(command.Plate) || motorcycle.Plate == command.Plate))
                {
                    motorcycle.Plate = command.Plate;
                    await _motorcycleRepository.Update(motorcycle);
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
            response.Success = false;
            try
            {
                if(!_rentalRepository.AnyValidRentalWithMorotcycle(command.Id, DateTime.UtcNow.Date))
                {
                    var motorcycle = _motorcycleRepository.GetById(command.Id);
                    await _motorcycleRepository.Delete(motorcycle);
                    response.Success = true;
                }
            }
            catch
            {
                response.Success = false;
            }

            return await Task.FromResult(response);
        }
    }
}
