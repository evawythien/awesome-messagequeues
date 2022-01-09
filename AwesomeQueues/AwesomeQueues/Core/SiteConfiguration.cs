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
                    .AddSingleton(provider => provider.GetRequiredService<IOptions<RabbitOptions>>().Value);

            services.AddSingleton(service =>
            {
                RabbitOptions options = service.GetRequiredService<RabbitOptions>();

                return new ConnectionFactory()
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    UserName = options.UserName,
                    Password = options.Password,
                    VirtualHost = options.VirtualHost
                };
            });

            services.AddScoped<IRabbitMQClient, RabbitMQClient>(service =>
            {
                ConnectionFactory factory = service.GetRequiredService<ConnectionFactory>();

                ILogger<RabbitMQClient> logger = service.GetService<ILogger<RabbitMQClient>>();
                return new RabbitMQClient(factory.CreateConnection(), logger);
            });

            services.AddHostedService(service =>
            {
                ConnectionFactory factory = service.GetRequiredService<ConnectionFactory>();

                ILogger<RabbitHostedService> logger = service.GetService<ILogger<RabbitHostedService>>();
                return new RabbitHostedService(factory.CreateConnection(), logger);
            });

            return services;
        }
    }
}
