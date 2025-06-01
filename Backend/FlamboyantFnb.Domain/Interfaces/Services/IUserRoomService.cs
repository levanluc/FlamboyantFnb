using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.RequestModel;
using FlamboyantFnb.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlamboyantFnb.Domain.Interfaces.Services
{
    public interface IUserRoomService
    {
        Task<UserRoom> AddMemberToRoomAsync(string roomId, string userId);
        Task<List<UserRoom>> GetLatestRoomByUserIdAsync(string userId);
    }
}
