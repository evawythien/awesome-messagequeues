using AwesomeQueues.Rabbit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace AwesomeQueues.Core
{
    public static class SiteConfiguration
    {

        public static IServiceCollection ConfigureRabbitMQ(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RabbitOptions>(config.GetSection("RabbitOptions"))
                    .AddScoped(provider => provider.GetRequiredService<IOptionsSnapshot<RabbitOptions>>().Value);


            services.AddScoped<IRabbitMQClient, RabbitMQClient>(services =>
            {
                RabbitOptions options = services.GetRequiredService<RabbitOptions>();

                ConnectionFactory factory = new ConnectionFactory()
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    UserName = options.UserName,
                    Password = options.Password,
                    VirtualHost = options.VirtualHost
                };

                ILogger<RabbitMQClient> logger = services.GetService<ILogger<RabbitMQClient>>();
                return new RabbitMQClient(factory.CreateConnection(), logger);
            });

            return services;
        }
    }
}
