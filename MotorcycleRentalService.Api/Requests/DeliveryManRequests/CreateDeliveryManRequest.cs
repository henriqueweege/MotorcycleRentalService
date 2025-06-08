using MotorcycleRentalService.Api.Validations;
using MotorcycleRentalService.Application.Commands.DeliveryManCommands;
using MotorcycleRentalService.Domain.Enums;

namespace MotorcycleRentalService.Api.Requests.DeliveryManRequests
{
    public class CreateDeliveryManRequest
    {
        public string identificador { get; set; }
        public string nome { get; set; }
        public string cnpj { get; set; }
        public DateTime data_nascimento { get; set; }
        public string numero_cnh { get; set; }
        public string tipo_cnh { get; set; }
        public string imagem_cnh { get; set; }

        public bool IsValid()
        {
            var validator = new DocumentInputValidator(imagem_cnh);

            return !string.IsNullOrWhiteSpace(identificador) &&
                !string.IsNullOrWhiteSpace(nome) &&
                !string.IsNullOrWhiteSpace(cnpj) &&
                !string.IsNullOrWhiteSpace(numero_cnh) &&
                validator.IsValid() &&
                (tipo_cnh == nameof(EDriverLicenseType.A) ||
                tipo_cnh == nameof(EDriverLicenseType.B) ||
                tipo_cnh == $"{nameof(EDriverLicenseType.A)}+{nameof(EDriverLicenseType.B)}");
        }

        public Create ToCommand()
        {
            return new Create()
            {
                Base64License = imagem_cnh,
                Birthday = data_nascimento,
                Cnpj = cnpj,
                Name = nome,
                Id = identificador,
                DriverLicenseType = tipo_cnh,
                DriverLicenseNumber = numero_cnh
            };
        }
    }
}
