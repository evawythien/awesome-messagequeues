using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeQueues.Rabbit
{
    public interface IRabbitMQClient
    {
        void Publish(string queue, string routingKey, string message);
    }
}
