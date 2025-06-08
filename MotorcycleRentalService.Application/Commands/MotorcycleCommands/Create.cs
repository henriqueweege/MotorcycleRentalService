using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Application.Commands.MotorcycleCommands
{
    public class Create
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
    }
}
