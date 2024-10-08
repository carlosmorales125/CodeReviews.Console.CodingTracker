using CodingTracker.Models;
using CodingTracker.Validators;
using Spectre.Console;

namespace CodingTracker.Services;

public static class MenuService
{
    public static void PresentWelcomeMessage()
    {
        AnsiConsole.Write(
            new FigletText("WELCOME TO THE CODING TRACKER!")
                .LeftJustified()
                .Color(Color.Green));
    }
    
    public static void PresentGoodbyeMessage()
    {
        AnsiConsole.Clear();
        
        AnsiConsole.Write(
            new FigletText("GOODBYE! AND THANKS FOR USING THE CODING TRACKER!")
                .LeftJustified()
                .Color(Color.Green));
    }
    
    public static MenuChoice PresentMenu()
    {
        var panel = new Panel(string.Join(Environment.NewLine, 
            "1. Add a coding session",
            "2. Edit a coding session",
            "3. Delete a coding session",
            "4. View all coding sessions",
            "5. Exit"));
            
        panel.Header("*  Coding Tracker Menu  *", Justify.Center);
        panel.Padding = new Padding(2, 1, 2, 1);
        panel.Border = BoxBorder.Double;
        panel.BorderStyle = new Style(foreground: Color.Green);
        
        var rule = new Rule("Select an option")
        {
            Justification = Justify.Left,
            Style = new Style(foreground: Color.Green)
        };
        
        while (true)
        {
            AnsiConsole.Write(panel);
            AnsiConsole.Write(rule);
            var choice = AnsiConsole.Prompt(
                new TextPrompt<string>(""));

            if (int.TryParse(choice, out var option))
            {
                switch (option)
                {
                    case 1:
                        return MenuChoice.AddCodingSession;
                    case 2:
                        return MenuChoice.EditCodingSession;
                    case 3:
                        return MenuChoice.DeleteCodingSession;
                    case 4:
                        return MenuChoice.ViewCodingSessions;
                    case 5:
                        return MenuChoice.Exit;
                }
            }
            
            DisplayInvalidInputMessage();
        }
    }

    public static CodingSession PresentAddCodingSessionMenu()
    {
        var startTime = PresentTimeInputMenu("start");
        long endTime;
        
        while (true)
        {
            endTime = PresentTimeInputMenu("end");
            
            if (TimeValidator.IsEndTimeGreaterThanStartTime(startTime, endTime))
            {
                break;
            }
            
            DisplayInvalidInputMessage("End time must be greater than start time. Please try again.");
        }
        
        return new CodingSession()
        {
            StartTime = startTime,
            EndTime = endTime
        };
    }
    
    public static int PresentGetIdMenu()
    {
        return AnsiConsole.Ask<int>("Enter the id:");
    }
    
    public static void PresentActionMessage(ActionMessage actionMessage)
    {
        AnsiConsole.Clear();
        
        var panel = new Panel(actionMessage.Message)
        {
            Border = BoxBorder.Double,
            Padding = new Padding(2, 1, 2, 1),
            BorderStyle = new Style(foreground: actionMessage.IsError ? Color.Red : Color.Green)
        };
        
        AnsiConsole.Write(panel);
    }
    
    public static CodingSession PresentEditCodingSessionMenu(int id)
    {
        var startTime = PresentTimeInputMenu("start");
        long endTime;
        
        do
        {
            endTime = PresentTimeInputMenu("end");
        } while (!TimeValidator.IsEndTimeGreaterThanStartTime(startTime, endTime));
        
        return new CodingSession()
        {
            Id = id,
            StartTime = startTime,
            EndTime = endTime
        };
    }

    private static long PresentTimeInputMenu(string startOrEnd)
    {
        int hour;
        int minute;
        string amOrPm;
        
        while (true)
        {
            hour = AnsiConsole.Ask<int>($"Enter the {startOrEnd} hour (between 1 and 12):");
            if (TimeValidator.IsHourValid(hour))
            {
                break;
            }
            DisplayInvalidInputMessage();
        }
        
        while (true)
        {
            minute = AnsiConsole.Ask<int>($"Enter the {startOrEnd} minute (between 0 and 59):");
            if (TimeValidator.IsMinuteValid(minute))
            {
                break;
            }
            DisplayInvalidInputMessage();
        }
        
        while (true)
        {
            amOrPm = AnsiConsole.Ask<string>("Enter AM or PM:");
            if (TimeValidator.IsAmPmValid(amOrPm))
            {
                break;
            }
            DisplayInvalidInputMessage();
        }
        
        if (TimeValidator.IsPm(amOrPm) && hour != 12)
        {
            hour += 12;
        }
        
        if (TimeValidator.IsAm(amOrPm) && hour == 12)
        {
            hour = 0;
        }
        
        var today = DateTime.Today;
        
        var startTime = new DateTime(today.Year, today.Month, today.Day, hour, minute, 0);

        return startTime.ToBinary();
    }
    
    public static void DisplayInvalidInputMessage(string message = "Invalid input. Please try again.")
    {
        AnsiConsole.Clear();
        
        var panel = new Panel(message)
        {
            Border = BoxBorder.Double,
            Padding = new Padding(2, 1, 2, 1),
            BorderStyle = new Style(foreground: Color.Red)
        };
        
        AnsiConsole.Write(panel);
    }
}