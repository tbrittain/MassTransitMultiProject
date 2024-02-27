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

                // No endpoints in this project, only in the Subscriber1 project
            });
        });

        return services;
    }
}