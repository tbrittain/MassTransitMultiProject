using MassTransit;
using MassTransitMultiProject.Shared.Contracts;
using MassTransitMultiProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMultiProject.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController(IPublishEndpoint bus) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Post([FromBody] PostMessageModel model)
    {
        var message = new Message(Guid.NewGuid(), model.Text, DateTime.Now);
        await bus.Publish(message,
            context =>
            {
                context.Headers.Set("x-message-version", "1.0.0");
            });
        return CreatedAtAction(nameof(Post), new { id = message.Id }, message);
    }
}