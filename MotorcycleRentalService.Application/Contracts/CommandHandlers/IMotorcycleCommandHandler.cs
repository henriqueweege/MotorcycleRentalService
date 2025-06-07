using MotorcycleRentalService.Application.Commands.MotorcycleCommands;
using MotorcycleRentalService.Application.Responses;

namespace MotorcycleRentalService.Application.Contracts.CommandHandlers
{
    public interface IMotorcycleCommandHandler
    {
        Task<CommandResponse> Handle(Create command);
        Task<CommandResponse> Handle(Update command);
        Task<CommandResponse> Handle(Delete command);
    }
}