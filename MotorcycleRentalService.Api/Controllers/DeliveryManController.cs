using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalService.Api.Requests.DeliveryManRequests;
using MotorcycleRentalService.Application.Commands.DeliveryManCommands;
using MotorcycleRentalService.Application.Contracts.CommandHandlers;

namespace MotorcycleRentalService.Api.Controllers
{
    [Tags("entregadores")]
    [ApiController]
    [AllowAnonymous]
#pragma warning disable 1591
    public class DeliveryManController : ControllerBase
    {
        private readonly IDeliveryManCommandHandler _commandHandler;
        public DeliveryManController(IDeliveryManCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost("/entregadores")]
        public async Task<IActionResult> Create([FromBody] CreateDeliveryManRequest request)
        {
            if (request.IsValid())
            {
                var command = request.ToCommand();

                var result = await _commandHandler.Handle(command);

                if (result.Success)
                {
                    return Created();
                }
            }

            return BadRequest(new { message = "Dados inválidos" });
        }

        [HttpPost("/entregadores/{id}/cnh")]
        public async Task<IActionResult> Create([FromBody] UpdateDeliveryManRequest request, [FromRoute] string id)
        {
            if (request.IsValid())
            {
                var command = new CreateDriverLicense()
                {
                    DeliveryManId = id,
                    Base64License = request.imagem_cnh
                };

                var result = _commandHandler.Handle(command);

                if (result.Success)
                {
                    return Created();
                }
            }

            return BadRequest(new { message = "Dados inválidos" });
        }
    }
}
