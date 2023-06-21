using MassTransit;
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

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(rabbitMq.ConnectionString), hostCfg =>
                    {
                        hostCfg.Username(rabbitMq.UserName);
                        hostCfg.Password(rabbitMq.Password);
                    });
                }));
            });
      
          //  services.AddSingleton<IBusControl>();

            return services;
        }
    }
}
