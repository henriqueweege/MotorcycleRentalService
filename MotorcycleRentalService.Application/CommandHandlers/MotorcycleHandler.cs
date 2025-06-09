using MassTransit;
using MotorcycleRentalService.Application.Commands.MotorcycleCommands;
using MotorcycleRentalService.Application.Contracts.CommandHandlers;
using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Domain.Events;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;
using Serilog;

namespace MotorcycleRentalService.Application.CommandHandlers
{
    public class MotorcycleHandler : IMotorcycleCommandHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public MotorcycleHandler(IMotorcycleRepository motorcycleRepository, IRentalRepository rentalRepository, IPublishEndpoint publishEndpoint)
        {
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
            _publishEndpoint = publishEndpoint;
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

                var @event = new MotorcycleCreated();
                @event.Id = motorcycle.Id;
                @event.MotorcycleYear = motorcycle.Year;

                await _publishEndpoint.Publish(@event);
                response.Success = true;
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
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
            catch (Exception ex)
            {
                Log.Information(ex.Message);
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
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                response.Success = false;
            }

            return await Task.FromResult(response);
        }
    }
}
