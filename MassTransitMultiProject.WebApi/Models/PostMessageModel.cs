using System.Text.Json.Serialization;

namespace MassTransitMultiProject.WebApi.Models;

public class PostMessageModel
{
    [JsonPropertyName("text")]
    public string Text { get; init; } = default!;
}