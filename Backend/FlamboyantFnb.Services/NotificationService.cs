using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.Interfaces.Repository;
using FlamboyantFnb.Domain.Interfaces.Services;
using FlamboyantFnb.Domain.RequestModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlamboyantFnb.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(IConfiguration configuration, INotificationRepository notificationRepository)
        {
            _configuration = configuration;
            _notificationRepository = notificationRepository;
        }

        public Task CreateAsync(MessageRequest req, string userId, string roomId)
        {
            return null;
        }

        public Task<List<Message>> GetByFilterAsync(MessageFilterRequest req)
        {
            return null;
        }
    }
}
