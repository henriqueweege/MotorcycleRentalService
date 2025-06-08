using MotorcycleRentalService.Application.Commands.RentalCommands;
using MotorcycleRentalService.Application.Responses;

namespace MotorcycleRentalService.Application.Contracts.CommandHandlers
{
    public interface IRentalCommandHandler
    {
        Task<CommandResponse> Handle(Create command);
        Task<CommandResponse> Handle(Update command);
    }
}
