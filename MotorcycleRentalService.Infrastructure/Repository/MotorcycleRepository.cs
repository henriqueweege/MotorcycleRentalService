using MotorcycleRentalService.Domain.Entities;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;

namespace MotorcycleRentalService.Infrastructure.Repository
{
    public class MotorcycleRepository : DefaultRepository<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(AppDbContext context) : base(context)
        {
        }

        public bool ExistsAnyByPlate(string plate)
        {
            return base.EntitySet.Any(x => x.Plate == plate);
        }

        public Motorcycle? GetByPlate(string plate)
        {
            return EntitySet.FirstOrDefault(x => x.Plate == plate);
        }
    }
}
