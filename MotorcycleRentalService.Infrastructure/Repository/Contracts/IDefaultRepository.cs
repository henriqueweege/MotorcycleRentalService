using MotorcycleRentalService.Domain.Contracts;

namespace MotorcycleRentalService.Infrastructure.Repository.Contracts
{
    public interface IDefaultRepository<T> where T : KeyedClass
    {
        Task<T> Add(T toAdd);
        T? GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Update(T toUpdate);
        Task<T> Delete(T toDelete);
    }
}
