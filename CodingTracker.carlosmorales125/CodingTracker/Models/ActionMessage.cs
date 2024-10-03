namespace CodingTracker.Models;

public class ActionMessage
{
    public required string Message { get; init; }
    public required bool IsError { get; init; }
}