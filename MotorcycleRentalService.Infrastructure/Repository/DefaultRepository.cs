
using Microsoft.EntityFrameworkCore;
using MotorcycleRentalService.Domain.Entities.Base;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;
using System.Linq.Expressions;
using System.Security.Principal;

namespace MotorcycleRentalService.Infrastructure.Repository
{
    public class DefaultRepository<T> : IDisposable, IDefaultRepository<T> where T : Entity
    {
        protected AppDbContext Context { get; set; }
        protected DbSet<T> EntitySet { get; set; }
        public DefaultRepository(AppDbContext context)
        {
            Context = context;
            EntitySet = Context.Set<T>();
        }

        public async Task<T> Update(T model)
        {
            Context.Update(model);
            if (await Context.SaveChangesAsync() > 0)
            {
                return model;
            }

            return null!;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task<T> Add(T toAdd)
        {
            Context.Add(toAdd);
            if (Context.SaveChanges() > 0)
            {
                return toAdd;
            }

            return null!;
        }
        public T? GetById(string id) => EntitySet.FirstOrDefault(x => x.Id == id);

        public async Task<IEnumerable<T>> GetAll() => await EntitySet.ToListAsync();

        public async Task<T> Delete(T toDelete)
        {
            Context.Remove(toDelete);

            if(await Context.SaveChangesAsync() > 0)
            {
                return toDelete;
            }
            throw new Exception();
        }
    }
}
