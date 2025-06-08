using MotorcycleRentalService.Domain.Entities.Base;

namespace MotorcycleRentalService.Domain.Entities
{
    public class Motorcycle : Entity
    {
        public string Model { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
    }
}
