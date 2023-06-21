using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.EventBus
{
    public static class Extension
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMq = new RabbitMqOption();
            configuration.GetSection("rabbitmq").Bind(rabbitMq);

            return services;
        }
    }
}
