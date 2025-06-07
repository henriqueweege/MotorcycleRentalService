namespace MotorcycleRentalService.Domain.Entities
{
    public class Rental
    {
        public string Id { get; set; }
        public string MotorcycleId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateOnly EffectiveEndDate { get; set; }
    }
}
