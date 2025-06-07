using MotorcycleRentalService.Application.Commands.DeliveryManCommands;
using MotorcycleRentalService.Application.Contracts.CommandHandlers;
using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;
using System.Text;

namespace MotorcycleRentalService.Application.CommandHandlers
{
    internal class DeliveryManHandler : IDeliveryManCommandHandler
    {
        private readonly IDefaultRepository<DeliveryMan> _repository;
        public DeliveryManHandler(IDefaultRepository<DeliveryMan> repository)
        {
            _repository = repository;
        }
        public async Task<CommandResponse> Create(Create command)
        {
            var response = new CommandResponse();

            try
            {
                response.Success = true;
                var deliveryMan = await _repository.Add(command.DeliveryMan);
                if (!CreateDriverLicense(new CreateDriverLicense() { Base64License = command.Base64License, DeliveryManId = deliveryMan.Id }).Success)
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

        public CommandResponse CreateDriverLicense(CreateDriverLicense command)
        {
            var response = new CommandResponse();

            try
            {
                var path = $"./{command.DeliveryManId}_{DateTime.UtcNow.Ticks}";
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(command.Base64License);
                    fs.Write(info, 0, info.Length);
                }

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
