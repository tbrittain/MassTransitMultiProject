using MassTransit;
using MassTransitMultiProject.Subscriber1.Consumers;

namespace MassTransitMultiProject.Subscriber1;

public static class DependencyInjection
{
    public static IServiceCollection AddMassTransitConsumers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<FirstConsumer>();
            x.AddConsumer<SecondConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var host = !string.IsNullOrWhiteSpace(configuration["RabbitMQ:Host"]) 
                    ? configuration["RabbitMQ:Host"] 
                    : "localhost";
                var port = !string.IsNullOrWhiteSpace(configuration["RabbitMQ:Port"]) 
                    ? configuration["RabbitMQ:Port"] 
                    : "5672";
                var username = !string.IsNullOrWhiteSpace(configuration["RabbitMQ:Username"]) 
                    ? configuration["RabbitMQ:Username"] 
                    : "guest";
                var password = !string.IsNullOrWhiteSpace(configuration["RabbitMQ:Password"]) 
                    ? configuration["RabbitMQ:Password"] 
                    : "guest";

                cfg.Host($"rabbitmq://{host}:{port}",
                    h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });
                
                // Important that the endpoints here hook into the same Message type as the one in the Shared.Contracts project
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}