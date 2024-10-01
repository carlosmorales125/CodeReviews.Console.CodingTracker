using Spectre.Console;

namespace CodingTracker;

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
        AnsiConsole.Write(
            new FigletText("Goodbye! THANKS FOR USING THE CODING TRACKER!")
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

        var errorRule = new Rule("Invalid choice. Please try again.")
        {
            Justification = Justify.Left,
            Style = new Style(foreground: Color.Red),
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
            AnsiConsole.Write(errorRule);
        }
    }

    public static CodingSession PresentAddCodingSessionMenu()
    {
        return new CodingSession
        {
            StartTime = AnsiConsole.Ask<int>("Enter the start time:"),
            EndTime = AnsiConsole.Ask<int>("Enter the end time:")
        };
    }
    
    public static int PresentGetIdMenu()
    {
        return AnsiConsole.Ask<int>("Enter the id:");
    }
}