
namespace MotorcycleRentalService.Application.Commands.DeliveryManCommands
{
    public class Create
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime Birthday { get; set; }
        public string DriverLicenseType { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string Base64License { get; set; }
    }
}
