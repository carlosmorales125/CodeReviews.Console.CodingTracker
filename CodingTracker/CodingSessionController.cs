namespace CodingTracker;

public class CodingSessionController(DbContext dbContext)
{
    public void AddCodingSession(CodingSession codingSession)
    {
        dbContext.AddCodingSession(codingSession);
    }
    
    public void EditCodingSession(CodingSession codingSession)
    {
        Console.WriteLine("Edit coding session");
    } 
    
    public void DeleteCodingSession(int id)
    {
        Console.WriteLine("Delete coding session");

    }
    
    public void ViewCodingSessions()
    {
        Console.WriteLine("View coding sessions");
    }
}