
using MotorcycleRentalService.Domain.Contracts;

namespace MotorcycleRentalService.Domain.Events
{
    public class MotorcycleCreated : KeyedClass
    {
        public int MotorcycleYear { get; set; }
    }
}
