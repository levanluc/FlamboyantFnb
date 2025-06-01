using Amazon.DynamoDBv2.DataModel;
using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.Interfaces.Repository;
using FlamboyantFnb.Domain.Interfaces.Services;
using FlamboyantFnb.Domain.RequestModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlamboyantFnb.Services
{
    public class MessageService : IMessageService
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IConfiguration configuration, IMessageRepository messageRepository)
        {
            _configuration = configuration;
            _messageRepository = messageRepository;
        }

        public Task CreateAsync(MessageRequest req, string userId, string roomId)
        {
            var msg = new Message()
            {
                Content = req.Content,
                ReceiverId = req.ReceiverId,
                SenderId = userId,
                RoomId = roomId,
                IsSeen = false
            };
            return _messageRepository.AddAsync(msg);
        }

        public Task<List<Message>> GetByFilterAsync(MessageFilterRequest req)
        {
            return _messageRepository.GetByFilterAsync(req);
        }
    }
}
