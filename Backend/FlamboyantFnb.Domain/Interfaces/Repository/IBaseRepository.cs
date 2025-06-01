using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlamboyantFnb.Domain.Interfaces.Repository
{
    public interface IBaseRepository<T>
    {
        Task<T> AddAsync(T entity, bool flush = true, CancellationToken cancellationToken = default);
        Task<T> DeleteAsync(T entity, bool flush = true, CancellationToken cancellationToken = default);
        IQueryable<T> GetAll();
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity, bool flush = true, CancellationToken cancellationToken = default);
    }
}
