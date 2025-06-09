using MotorcycleRentalService.Domain.Contracts;

namespace MotorcycleRentalService.Domain.Entities
{
    public class Motorcycle : KeyedClass
    {
        public string Model { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
    }
}
