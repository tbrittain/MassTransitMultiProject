using MassTransit;
using MassTransitMultiProject.Shared.Contracts;

namespace MassTransitMultiProject.Subscriber1.Consumers;

public class FirstConsumer(ILogger<FirstConsumer> logger) : IConsumer<Message>
{
    public Task Consume(ConsumeContext<Message> context)
    {
        var message = context.Message;
        var headers = context.Headers;
        var messageVersion = headers.Get<string>("x-message-version");
        logger.LogInformation("First consumer received message: {Message} with version {Version}", message, messageVersion);
        return Task.CompletedTask;
    }
}