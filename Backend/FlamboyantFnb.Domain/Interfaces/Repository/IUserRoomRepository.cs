using FlamboyantFnb.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlamboyantFnb.Domain.Interfaces.Repository
{
    public interface IUserRoomRepository : IBaseRepository<UserRoom>
    {
        Task<List<UserRoom>> GetByUserIdAsync(string userId);
    }
}
