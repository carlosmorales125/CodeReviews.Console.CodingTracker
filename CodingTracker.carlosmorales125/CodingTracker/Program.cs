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
            var addNewCoddingSessionMessage = codingSessionController.AddCodingSession(newCodingSession);
            MenuService.PresentActionMessage(addNewCoddingSessionMessage);
            break;
        case MenuChoice.EditCodingSession:
            var editCodingSessionId = MenuService.PresentGetIdMenu();
            var codingSessionExists = codingSessionController.CheckIfCodingSessionExists(editCodingSessionId);
            if (codingSessionExists.IsError)
            {
                MenuService.PresentActionMessage(codingSessionExists);
                break;
            }
            var editCodingSession = MenuService.PresentEditCodingSessionMenu(editCodingSessionId);
            var editCodingSessionMessage = codingSessionController.EditCodingSession(editCodingSession);
            MenuService.PresentActionMessage(editCodingSessionMessage);
            break;
        case MenuChoice.DeleteCodingSession:
            var id = MenuService.PresentGetIdMenu();
            var deleteCodingSessionActionMessage = codingSessionController.DeleteCodingSession(id);
            MenuService.PresentActionMessage(deleteCodingSessionActionMessage);
            break;
        case MenuChoice.ViewCodingSessions:
            codingSessionController.ViewCodingSessions();
            break;
        case MenuChoice.Exit:
            MenuService.PresentGoodbyeMessage();
            return;
        default:
            Console.Clear();
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}