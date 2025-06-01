using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.RequestModel;
using FlamboyantFnb.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlamboyantFnb.Domain.Interfaces.Services
{
    public interface IMessageService
    {
        Task CreateAsync(MessageRequest req, string userId, string roomId);
        Task<List<Message>> GetByFilterAsync(MessageFilterRequest req);
    }
}
