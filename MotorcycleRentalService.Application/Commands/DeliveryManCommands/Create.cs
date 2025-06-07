using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Application.Commands.DeliveryManCommands
{
    public class Create
    {
        public DeliveryMan DeliveryMan { get; set; }
        public string Base64License { get; set; }
    }
}
