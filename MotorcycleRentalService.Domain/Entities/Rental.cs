using MotorcycleRentalService.Domain.Entities.Base;

namespace MotorcycleRentalService.Domain.Entities
{
    public class Rental : Entity
    {
        public string MotorcycleId { get; set; }
        public string DeliveryManId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public int MaxDays { get; set; }

        public int GetTotalRentalDays() => (EffectiveEndDate - StartDate).Days;
        public int GetTotalOverdueDays() => (EffectiveEndDate - EndDate).Days;

        public int GetDailyFee()
        {
            switch (MaxDays)
            {
                case 7:
                    return 30;
                case 15:
                    return 28;
                case 30:
                    return 22;
                case 45:
                    return 20;
                case 50:
                    return 18;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
