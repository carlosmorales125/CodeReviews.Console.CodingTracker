namespace CodingTracker.Models;

public class CodingSession
{
    public int Id { get; init; }
    public long StartTime { get; init; }
    public long EndTime { get; init; }
    public long Duration => EndTime - StartTime;
}