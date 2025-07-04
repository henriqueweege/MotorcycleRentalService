﻿using MotorcycleRentalService.Application.Commands.DeliveryManCommands;
using MotorcycleRentalService.Application.Responses;

namespace MotorcycleRentalService.Application.Contracts.CommandHandlers
{
    public interface IDeliveryManCommandHandler
    {
        Task<CommandResponse> Handle(Create command);
        CommandResponse Handle(CreateDriverLicense command);
    }
}
