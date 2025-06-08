using MotorcycleRentalService.Application.Commands.DeliveryManCommands;
using MotorcycleRentalService.Application.Contracts.CommandHandlers;
using MotorcycleRentalService.Application.Responses;
using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;
using System.Text;

namespace MotorcycleRentalService.Application.CommandHandlers
{
    public class DeliveryManHandler : IDeliveryManCommandHandler
    {
        private readonly IDefaultRepository<DeliveryMan> _repository;
        public DeliveryManHandler(IDefaultRepository<DeliveryMan> repository)
        {
            _repository = repository;
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
                string[] fileList = GetOldVersions(command);

                string path = CreateLicense(command);

                RemoveOldVersions(fileList);

                response.Success = File.Exists(path);
            }
            catch
            {
                response.Success = false;
            }

            return response;
        }

        private static string[] GetOldVersions(CreateDriverLicense command)
        {
            string filesToDelete = $@"{command.DeliveryManId}*";
            string[] fileList = System.IO.Directory.GetFiles("./", filesToDelete);
            return fileList;
        }

        private static string CreateLicense(CreateDriverLicense command)
        {
            var path = $"./{command.DeliveryManId}_{DateTime.UtcNow.Ticks}.txt";
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(command.Base64License);
                fs.Write(info, 0, info.Length);
            }

            return path;
        }

        private static void RemoveOldVersions(string[] fileList)
        {
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
        }
    }
}
