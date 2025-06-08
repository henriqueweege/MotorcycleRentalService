using MotorcycleRentalService.Application.Commands.DeliveryManCommands;
using MotorcycleRentalService.Application.Contracts.CommandHandlers;
using MotorcycleRentalService.Application.Contracts.Services;
using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;

namespace MotorcycleRentalService.Application.CommandHandlers
{
    public class DeliveryManHandler : IDeliveryManCommandHandler
    {
        private readonly IDefaultRepository<DeliveryMan> _repository;
        private readonly IDocumentSaver _documentSaver;
        public DeliveryManHandler(IDefaultRepository<DeliveryMan> repository, IDocumentSaver documentSaver)
        {
            _repository = repository;
            _documentSaver = documentSaver;
        }
        public async Task<CommandResponse> Handle(Create command)
        {
            var response = new CommandResponse();

            try
            {
                response.Success = true;
                var deliveryMan = new DeliveryMan()
                {
                    Id = command.Id,
                    Name = command.Name,
                    Cnpj = command.Cnpj,
                    Birthday = command.Birthday,
                    DriverLicenseNumber = command.DriverLicenseNumber
                };

                deliveryMan.DriverLicenseType = DeliveryMan.GetLicenseTypeFromString(command.DriverLicenseType);

                deliveryMan = await _repository.Add(deliveryMan);
                if (deliveryMan is not null && !Handle(new CreateDriverLicense() { Base64License = command.Base64License, DeliveryManId = deliveryMan.Id }).Success)
                {
                    await _repository.Delete(deliveryMan);
                    response.Success = false;
                }
            }
            catch
            {
                response.Success = false;
            }

            return response;
        }

        public CommandResponse Handle(CreateDriverLicense command)
        {
            var response = new CommandResponse();

            try
            {
                string path = _documentSaver.SaveDocument(command);
                response.Success = File.Exists(path);
            }
            catch
            {
                response.Success = false;
            }

            return response;
        }
    }
}
