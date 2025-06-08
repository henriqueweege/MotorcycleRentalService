using MotorcycleRentalService.Application.Commands.MotorcycleCommands;

namespace MotorcycleRentalService.Api.Requests.MotorcycleRequests
{
    public class UpdateMotorcycleRequest
    {
        public string placa { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(placa);
        }

        public Update ToCommand(string id)
        {
            return new Update() { Plate = placa, Id = id };
        }
    }
}
