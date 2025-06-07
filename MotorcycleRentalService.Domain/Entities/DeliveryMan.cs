using MotorcycleRentalService.Domain.ValueObjects;

namespace MotorcycleRentalService.Domain.Entities
{
    public class DeliveryMan
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Cnpj Cnpj { get; set; }
        public DateOnly Birthday { get; set; }
        public DriverLicense DriverLicense { get; set; }
    }
}
