using MotorcycleRentalService.Application.Commands.RentalCommands;
using MotorcycleRentalService.Application.Contracts.CommandHandlers;
using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;
using Serilog;

namespace MotorcycleRentalService.Application.CommandHandlers
{
    public class RentalHandler : IRentalCommandHandler
    {
        private readonly IDefaultRepository<Rental> _repository;
        private readonly IDefaultRepository<Motorcycle> _motorcycleRepository;
        private readonly IDefaultRepository<DeliveryMan> _deliveryManRepository;

        public RentalHandler(
        IDefaultRepository<Rental> repository,
        IDefaultRepository<Motorcycle> motorcycleRepository,
        IDefaultRepository<DeliveryMan> deliveryManRepository)
        {
            _repository = repository;
            _motorcycleRepository = motorcycleRepository;
            _deliveryManRepository = deliveryManRepository;
        }

        public async Task<CommandResponse> Handle(Create command)
        {
            var response = new CommandResponse();
            try
            {
                response.Success = false;

                var deliveryMan = _deliveryManRepository.GetById(command.DeliveryManId);
                var motorCycle = _motorcycleRepository.GetById(command.MotorcycleId);


                if (motorCycle is not null && deliveryMan is not null && ((deliveryMan.DriverLicenseType & Domain.Enums.EDriverLicenseType.A) > 0 ))
                {
                    var rental = new Rental();

                    rental.Id = string.IsNullOrWhiteSpace(command.Id) ? Guid.NewGuid().ToString() : command.Id;
                    rental.MotorcycleId = command.MotorcycleId;
                    rental.DeliveryManId = command.DeliveryManId;
                    rental.StartDate = command.StartDate;
                    rental.EndDate = rental.StartDate.AddDays(command.MaxDays);
                    rental.EffectiveEndDate = command.EndDate;
                    rental.MaxDays = command.MaxDays;

                    await _repository.Add(rental);

                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                response.Success = false;
            }

            return response;
        }

        public async Task<CommandResponse> Handle(Update command)
        {
            var response = new CommandResponse();
            try
            {
                response.Success = false;

                var rental = _repository.GetById(command.RentalId);
                if (rental is not null && command.EffectiveEndDate >= rental.StartDate)
                {
                    rental.EffectiveEndDate = command.EffectiveEndDate;

                    await _repository.Update(rental);

                    response.Success = true;
                }
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
