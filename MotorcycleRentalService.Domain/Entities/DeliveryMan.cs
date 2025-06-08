using MotorcycleRentalService.Domain.Entities.Base;
using MotorcycleRentalService.Domain.Enums;

namespace MotorcycleRentalService.Domain.Entities
{
    public class DeliveryMan : Entity
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime Birthday { get; set; }
        public EDriverLicenseType DriverLicenseType { get; set; }
        public string DriverLicenseNumber { get; set; }

        public static EDriverLicenseType GetLicenseTypeFromString(string licenseType)
        {
            switch (licenseType.ToUpper())
            {
                case "A":
                    return EDriverLicenseType.A;
                case "B":
                    return EDriverLicenseType.B;
                default:
                    return EDriverLicenseType.A | EDriverLicenseType.B;
            }
        }
    }
}
