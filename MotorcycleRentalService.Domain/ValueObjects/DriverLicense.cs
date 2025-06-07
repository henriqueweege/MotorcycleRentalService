using MotorcycleRentalService.Domain.Enums;

namespace MotorcycleRentalService.Domain.ValueObjects
{
    public class DriverLicense : Document
    {
        public EHabilitationType HabilitationType { get; set; }
        public string Number { get; set; }
    }
}
