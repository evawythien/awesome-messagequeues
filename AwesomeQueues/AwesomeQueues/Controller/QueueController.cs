using AwesomeQueues.Rabbit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AwesomeQueues.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IRabbitMQClient client;

        public QueueController(IRabbitMQClient client)
        {
            this.client = client;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post()
        {
            this.client.Publish("queue-post", "postkey","messageeeee");
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
