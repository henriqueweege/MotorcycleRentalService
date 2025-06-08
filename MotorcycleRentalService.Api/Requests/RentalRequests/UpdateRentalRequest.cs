using MotorcycleRentalService.Application.Commands.RentalCommands;

namespace MotorcycleRentalService.Api.Requests.RentalRequests
{
    public class UpdateRentalRequest
    {
        public DateTime data_devolucao { get; set; }

        public Update ToCommand(string id)
        {
            return new Update()
            {
                EffectiveEndDate = data_devolucao,
                RentalId = id
            };
        }
    }
}
