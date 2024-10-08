namespace CodingTracker.Models;

public class CodingSession
{
    public int Id { get; init; }
    public long StartTime { get; init; }
    public long EndTime { get; init; }
    public string Duration
    {
        get
        {
            var startTime = DateTime.FromBinary(StartTime);
            var endTime = DateTime.FromBinary(EndTime);
            var duration = endTime - startTime;
            return duration.ToString(@"hh\:mm");
        }
    }
}