using MotorcycleRentalService.Application.Commands.DeliveryManCommands;

namespace MotorcycleRentalService.Application.Contracts.Services
{
    public interface IDocumentSaver
    {
        string SaveDocument(CreateDriverLicense command);
    }
}
