using MassTransit;

namespace MassTransitMultiProject.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((_, cfg) =>
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

                // No endpoints in this project, only in the Subscriber1 project
            });
        });

        return services;
    }
}