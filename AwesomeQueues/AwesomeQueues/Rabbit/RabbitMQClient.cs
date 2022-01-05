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

        public void Publish(string queue, string routingKey, string message)
        {
            this.logger.LogInformation($"PushMessage, routingKey: {routingKey}");
            this.channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            this.channel.BasicPublish(exchange: "message", routingKey: routingKey, basicProperties: null, body: body);
        }
    }
}
