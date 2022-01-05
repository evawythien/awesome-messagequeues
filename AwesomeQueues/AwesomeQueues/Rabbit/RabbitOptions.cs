namespace AwesomeQueues.Rabbit
{
    /// <summary>
    /// RabbitMQ configuration.
    /// </summary>
    public class RabbitOptions
    {
        /// <summary>
        /// Host name.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Virtual host.
        /// </summary>
        public string VirtualHost { get; set; }
    }
}
