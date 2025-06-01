using Amazon.DynamoDBv2.DataModel;
using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using FlamboyantFnb.Domain.Interfaces.Repository;

namespace FlamboyantFnb.Services
{
    public class RoomService : IRoomService
    {
        private readonly IConfiguration _configuration;
        private readonly IDynamoDBContext _context;
        private readonly IRoomRepository _roomRepository;

        public RoomService(IConfiguration configuration, IDynamoDBContext context, IRoomRepository roomRepository)
        {
            _configuration = configuration;
            _context = context;
            _roomRepository = roomRepository;
        }

        public Task<Room> GetByIdAsync(string id)
        {
            return _roomRepository.GetByIdAsync(id);
        }

        public async Task<Room> CreateRoomAsync(string id, string roomName) {
            var existingRoom = await _roomRepository.GetByIdAsync(id);
            if (existingRoom == null)
            {
                var newRoom = new Room()
                {
                    Id = id,
                    RoomName = roomName
                };
                await _roomRepository.AddAsync(newRoom);
                return newRoom;
            }
            return existingRoom;
        }


        public Task UpdateRoomAsync(Room room)
        {
            return _roomRepository.UpdateAsync(room);
        }
    }
}
