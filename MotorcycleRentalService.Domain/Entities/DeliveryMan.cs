using MotorcycleRentalService.Domain.Constants;
using Newtonsoft.Json;

namespace MotorcycleRentalService.Domain.Entities
{
    public class DeliveryMan : User
    {
        public string Id { get; set; }
        public string Cnpj { get; set; }
        public string MyProperty { get; set; }
    }
}
