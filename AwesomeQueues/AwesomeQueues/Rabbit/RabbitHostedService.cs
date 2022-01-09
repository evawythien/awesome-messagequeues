using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeQueues.Rabbit
{
    public class RabbitHostedService : IHostedService
    {
        /// <summary>
        /// Conexión.
        /// </summary>
        protected IConnection connection;

        /// <summary>
        /// Channel.
        /// </summary>
        protected IModel channel;

        /// <summary>
        /// Logger.
        /// </summary>
        protected readonly ILogger<RabbitHostedService> logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="logger"></param>
        public RabbitHostedService(IConnection connection, ILogger<RabbitHostedService> logger)
        {
            this.connection = connection;
            this.logger = logger;
            this.channel = connection.CreateModel();
        }

        private const string routingKey = "awesome-key";
        private const string queue = "queue-post";

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"RoutingKey: {routingKey}");

            EventingBasicConsumer consumer = new EventingBasicConsumer(this.channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);

                this.logger.LogInformation("Received {0}", message);

                this.channel.BasicAck(ea.DeliveryTag, false);
            };

            this.channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
