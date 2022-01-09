using AwesomeQueues.Rabbit;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeQueues.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        /// <summary>
        /// RabbitMQ cliente.
        /// </summary>
        private readonly IRabbitMQClient client;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client"></param>
        public QueueController(IRabbitMQClient client)
        {
            this.client = client;
        }

        [HttpPost]
        public void Post()
        {
            this.client.Publish("queue-post", "awesome-key", "messageeeee");
        }
    }
}
