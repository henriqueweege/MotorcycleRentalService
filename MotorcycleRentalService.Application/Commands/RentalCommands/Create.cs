using MotorcycleRentalService.Domain.Entities;

namespace MotorcycleRentalService.Application.Commands.RentalCommands
{
    public class Create
    {
        public string Id { get; set; }
        public string DeliveryManId { get; set; }
        public string MotorcycleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public int DailyFee { get; set; }
        public int MaxDays { get; set; }
    }
}
