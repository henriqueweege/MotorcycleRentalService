using MotorcycleRentalService.Application.Commands.MotorcycleCommands;

namespace MotorcycleRentalService.Api.Requests.MotorcycleRequests
{
    public class CreateMotorcycleRequest
    {
        public string identificador { get; set; }
        public int ano { get; set; }
        public string modelo { get; set; }
        public string placa { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(placa) && !string.IsNullOrWhiteSpace(modelo) && !string.IsNullOrWhiteSpace(identificador);
        }

        public Create ToCommand()
        {
            return new Create()
            {
                Id = identificador,
                Year = ano,
                Model = modelo,
                Plate = placa
            };
        }
    }
}
