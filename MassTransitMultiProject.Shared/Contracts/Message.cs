namespace MassTransitMultiProject.Shared.Contracts;

public record Message(Guid Id, string Text, DateTime CreatedAt);