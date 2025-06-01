using FlamboyantFnb.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlamboyantFnb.Domain.Interfaces.Services
{
    public interface IRoomService
    {
        Task<Room> GetByIdAsync(string id);
        Task<Room> CreateRoomAsync(string id, string roomName);
        Task UpdateRoomAsync(Room room);
    }
}
