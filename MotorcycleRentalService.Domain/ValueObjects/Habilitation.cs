using MotorcycleRentalService.Domain.Enums;

namespace MotorcycleRentalService.Domain.ValueObjects
{
    public class Habilitation : Document
    {
        public EHabilitationType HabilitationType { get; set; }
        public string HabilitationImage { get; set; }
    }
}
