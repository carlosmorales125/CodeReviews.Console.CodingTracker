namespace CodingTracker.Models;

public class CodingSession
{
    public int Id { get; init; }
    public int StartTime { get; init; }
    public int EndTime { get; init; }
    public int Duration => EndTime - StartTime;
}