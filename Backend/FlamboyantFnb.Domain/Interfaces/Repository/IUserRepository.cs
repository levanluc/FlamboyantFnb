using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.RequestModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlamboyantFnb.Domain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByUserNameAsync(string username);
    }
}
