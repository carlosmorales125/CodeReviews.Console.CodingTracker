using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker.Controllers;

public class CodingSessionController(DbContext dbContext)
{
    public ActionMessage AddCodingSession(CodingSession codingSession)
    {
        return dbContext.AddCodingSession(codingSession);
    }
    
    public ActionMessage EditCodingSession(CodingSession codingSession)
    {
        return dbContext.EditCodingSession(codingSession);
    } 
    
    public ActionMessage DeleteCodingSession(int id)
    {
        return dbContext.CheckIfIdExist(id).IsError 
            ? dbContext.CheckIfIdExist(id) 
            : dbContext.DeleteCodingSession(id);
    }
    
    public void ViewCodingSessions()
    {
        var codingSessions = dbContext.GetCodingSessions();

        if (codingSessions.Count == 0)
        {
            var noCodingSessionsMessage = new Panel("No coding sessions found")
            {
                Border = BoxBorder.Double,
                Padding = new Padding(2, 1, 2, 1),
                BorderStyle = new Style(foreground: Color.Red)
            };
            
            AnsiConsole.Write(noCodingSessionsMessage);
        }
        else
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn("Duration");

            foreach (var codingSession in codingSessions)
            {
                table.AddRow(codingSession.Id.ToString(), codingSession.StartTime.ToString(), codingSession.EndTime.ToString(), codingSession.Duration.ToString());
            }

            AnsiConsole.Write(table);
        }
    }
    
    public ActionMessage CheckIfCodingSessionExists(int id)
    {
        return dbContext.CheckIfIdExist(id);
    }
}