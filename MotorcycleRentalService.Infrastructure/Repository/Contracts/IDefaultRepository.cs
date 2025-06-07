using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRentalService.Infrastructure.Repository.Contracts
{
    public interface IDefaultRepository<T> where T : class
    {
        Task<T> Add(T toAdd);
        Task<T?> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Update(T toUpdate);
        Task<T> Delete(T toDelete);
    }
}
