using MotorcycleRentalService.Api.Validations;

namespace MotorcycleRentalService.Api.Requests.DeliveryManRequests
{
    public class UpdateDeliveryManRequest
    {
        public string imagem_cnh { get; set; }

        public bool IsValid()
        {
            var validator = new DocumentInputValidator(imagem_cnh);
            return validator.IsValid();
        }
    }
}
