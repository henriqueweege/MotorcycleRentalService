using MotorcycleRentalService.Domain.Enums;

namespace MotorcycleRentalService.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateOnly Birthday { get; set; }
        public EUserType UserType { get; set; }
    }
}
