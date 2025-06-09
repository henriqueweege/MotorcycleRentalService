using MotorcycleRentalService.Application.Commands.RentalCommands;

namespace MotorcycleRentalService.Api.Requests.RentalRequests
{
    public class CreateRentalRequest
    {
        public string? identificador { get; set; }
        public string entregador_id { get; set; }
        public string moto_id { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_termino { get; set; }
        public DateTime data_previsao_termino { get; set; }
        public int plano { get; set; }

        public bool IsValid()
        {

            return !string.IsNullOrWhiteSpace(entregador_id) &&
                !string.IsNullOrWhiteSpace(moto_id) &&
                (plano == 7 || plano == 15 || plano == 30 || plano == 45 || plano == 50) &&
                data_inicio.AddDays(plano) == data_termino &&
                data_inicio.AddDays(plano) == data_previsao_termino;

        }

        public Create ToCommand()
        {
            return new Create
            {
                Id = identificador is null ? string.Empty : identificador,
                DeliveryManId = entregador_id,
                MotorcycleId = moto_id,
                StartDate = data_inicio,
                EndDate = data_termino,
                EffectiveEndDate = data_previsao_termino,
                MaxDays = plano
            };
        }
    }
}
