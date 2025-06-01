using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlamboyantFnb.Domain.Interfaces.MessageQueue
{
    public interface IKafkaProducer
    {
        Task ProduceAsync<T>(string topic, string key, T value);
    }
}
