namespace CodingTracker.Validators;

public class TimeValidator
{
    public static bool IsHourValid(int hour) => hour is >= 0 and <= 12;
    
    public static bool IsMinuteValid(int minute) => minute is >= 0 and <= 59;
    
    public static bool IsAmPmValid(string amPm) => amPm.ToUpper() is "AM" or "PM";
    
    public static bool IsPm(string pm) => pm.ToUpper() is "PM";
    
    public static bool IsEndTimeGreaterThanStartTime(long startTime, long endTime) => endTime > startTime;
}