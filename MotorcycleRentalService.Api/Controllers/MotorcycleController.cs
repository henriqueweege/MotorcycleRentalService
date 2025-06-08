using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalService.Application.Contracts.CommandHandlers;
using MotorcycleRentalService.Application.Contracts.QueryHandlers;
using MotorcycleRentalService.Api.Requests.MotorcycleRequests;
using MotorcycleRentalService.Application.Commands.MotorcycleCommands;

namespace MotorcycleRentalService.Api.Controllers
{
    [Tags("motos")]
    [ApiController]
    [AllowAnonymous]
#pragma warning disable 1591
    public class MotorcycleController : ControllerBase
    {
        private readonly IMotorcycleCommandHandler _commandHandler;
        private readonly IMotorcycleQueryHandler _queryHandler;
        public MotorcycleController(IMotorcycleCommandHandler commandHandler, IMotorcycleQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpPost("/motos")]
        public async Task<IActionResult> Create([FromBody] CreateMotorcycleRequest request)
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

        [HttpGet("/motos")]
        public async Task<IActionResult> Get([FromQuery] string? plate)
        {
            var response = await _queryHandler.GetByPlate(plate);
            return Ok(response.Objects.Select(x => new { identificador = x.Id, ano = x.Year, modelo = x.Model, placa = x.Plate }));
        }

        [HttpPut("motos/{id}/placa")]
        public async Task<IActionResult> Update([FromRoute]string id, [FromBody] UpdateMotorcycleRequest request)
        {
            if (request.IsValid())
            {
                var command = request.ToCommand(id);

                var result = await _commandHandler.Handle(command);

                if (result.Success)
                {
                    return Ok(new { message = "Placa modificada com sucesso" });

                }
            }

            return BadRequest(new { message = "Dados inválidos" });
        }

        [HttpGet("/motos/{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { message = "Request mal formada" });
            }
            var result = await _queryHandler.GetById(id);

            if (result.Success)
            {
                return Ok(result.Objects.Select(x=> new { identificador = x.Id, ano = x.Year, modelo = x.Model, placa = x.Plate }).First());
            }

            return NotFound(new { message = "Moto não encontrada" });
        }

        [HttpDelete("/motos/{id}")]
        public async Task<IActionResult> Remove([FromRoute] string id)
        {
            var command = new Delete() { Id = id };
            var result = await _commandHandler.Handle(command);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(new { message = "Dados inválidos" });
        }
        ///// <summary>
        ///// Create a User.
        ///// </summary>
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(CommandResult<User>), StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(typeof(CommandResult<User>), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(CommandResult<User>), StatusCodes.Status200OK)]
        //[HttpPost]
        //public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, [FromServices] IMediator mediator)
        //    => await RestTools<User>.Return(await mediator.Send(command));

    }
}
