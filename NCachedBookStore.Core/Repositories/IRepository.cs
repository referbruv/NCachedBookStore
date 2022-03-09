using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCachedBookStore.Contracts.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        int Count();
    }
}
