using CodingTracker;
using CodingTracker.Controllers;
using CodingTracker.Services;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var dbContext = new DbContext(configuration);
dbContext.CreateDatabase();

var codingSessionController = new CodingSessionController(dbContext);

MenuService.PresentWelcomeMessage();

while (true)
{
    var choice = MenuService.PresentMenu();
    switch (choice)
    {
        case MenuChoice.AddCodingSession:
            var newCodingSession = MenuService.PresentAddCodingSessionMenu();
            codingSessionController.AddCodingSession(newCodingSession);
            break;
        case MenuChoice.EditCodingSession:
            
            //codingSessionController.EditCodingSession(editCodingSession);
            break;
        case MenuChoice.DeleteCodingSession:
            var id = MenuService.PresentGetIdMenu();
            codingSessionController.DeleteCodingSession(1);
            break;
        case MenuChoice.ViewCodingSessions:
            codingSessionController.ViewCodingSessions();
            break;
        case MenuChoice.Exit:
            MenuService.PresentGoodbyeMessage();
            return;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}