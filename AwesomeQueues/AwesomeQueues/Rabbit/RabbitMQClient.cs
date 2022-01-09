using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace AwesomeQueues.Rabbit
{
    public class RabbitMQClient : IRabbitMQClient
    {
        protected IConnection connection;

        /// <summary>
        /// Channel
        /// </summary>
        protected IModel channel;

        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger<RabbitMQClient> logger;

        public RabbitMQClient(IConnection connection, ILogger<RabbitMQClient> logger)
        {
            this.connection = connection;
            this.logger = logger;
            this.channel = connection.CreateModel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="routingKey"></param>
        /// <param name="message"></param>
        public void Publish<T>(string queue, string routingKey, T message)
        {
            this.logger.LogInformation($"Queue: {queue}");
            this.logger.LogInformation($"RoutingKey: {routingKey}");
            this.channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            RabbitMessage<T> rabbitMessage = new RabbitMessage<T>("CREATE", message);
            byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rabbitMessage));

            this.channel.BasicPublish(exchange: "awesome.exchange", routingKey: routingKey, basicProperties: null, body: body);
        }
    }
}
