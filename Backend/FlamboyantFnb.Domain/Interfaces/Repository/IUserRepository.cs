using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.RequestModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlamboyantFnb.Domain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<List<User>> GetAllAsync(UserFilterReq req, CancellationToken cancellationToken = default);
    }
}
