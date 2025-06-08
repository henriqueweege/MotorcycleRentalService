using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalService.Api.Requests.RentalRequests;
using MotorcycleRentalService.Application.Contracts.CommandHandlers;
using MotorcycleRentalService.Application.Contracts.QueryHandlers;
using System.Net;

namespace MotorcycleRentalService.Api.Controllers
{
    [Tags("locação")]
    [ApiController]
    [AllowAnonymous]
#pragma warning disable 1591
    public class RentalController : ControllerBase
    {
        private readonly IRentalCommandHandler _commandHandler;
        private readonly IRentalQueryHandler _queryHandler;
        public RentalController(IRentalCommandHandler commandHandler, IRentalQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpPost("/locacao")]
        public async Task<IActionResult> Create([FromBody] CreateRentalRequest request)
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

        [HttpGet("/locacao/{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { message = "Dados inválidos" });
            }
            var result = await _queryHandler.GetById(id);

            if (result.Success)
            {
                return Ok(result.Objects.Select(x => new
                {
                    identificador = x.Id,
                    valor_diaria = x.GetDailyFee(),
                    entregador_id = x.DeliveryManId,
                    moto_id = x.MotorcycleId,
                    data_inicio = x.StartDate,
                    data_termino = x.EndDate,
                    data_previsao_termino = x.StartDate.AddDays(x.MaxDays),
                    data_devolucao = x.EffectiveEndDate

                }).First());
            }

            return NotFound(new { message = "Locação não encontrada" });
        }

        [HttpGet("/locacao/{id}/custo")]
        public async Task<IActionResult> GetTotalCost([FromRoute] string id)
        {

            var result = await _queryHandler.GetTotalCost(id);

            if (result.Success)
            {
                var totalCost = result.Objects.First().TotalCost;
                return Ok(new
                {
                    custo_total = totalCost

                });
            }

            return NotFound(new { message = "Locação não encontrada" });
        }

        [HttpPut("/locacao/{id}/devolucao")]
        public async Task<IActionResult> Update([FromBody] UpdateRentalRequest request, [FromRoute] string id)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { message = "Dados inválidos" });
            }

            var command = request.ToCommand(id);
            var response = await _commandHandler.Handle(command);

            if (response.Success)
            {
                return Ok(new { message = "Data de devolução informada com sucesso" });
            }

            return StatusCode(500);
        }
    }
}
