namespace AwesomeQueues.Rabbit
{
    public interface IRabbitMQClient
    {
        void Publish<T>(string queue, string routingKey, T message);
    }
}
