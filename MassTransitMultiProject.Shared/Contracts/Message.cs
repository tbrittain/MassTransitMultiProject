namespace MassTransitMultiProject.Shared.Contracts;

public record Message(Guid Id, string Text, DateTime CreatedAt)
{
    override public string ToString()
    {
        return $"Id: {Id}, Text: {Text}, CreatedAt: {CreatedAt}";
    }
}