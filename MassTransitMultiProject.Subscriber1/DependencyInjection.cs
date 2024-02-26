using MassTransit;

namespace MassTransitMultiProject.Subscriber1;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var host = configuration["RabbitMQ:Host"] ?? "localhost";
                var port = configuration["RabbitMQ:Port"] ?? "5672";
                var username = configuration["RabbitMQ:Username"] ?? "guest";
                var password = configuration["RabbitMQ:Password"] ?? "guest";

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