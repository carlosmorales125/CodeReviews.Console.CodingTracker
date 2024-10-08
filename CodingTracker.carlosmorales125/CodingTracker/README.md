# Console.CodingTracker

## Description
This is a simple Coding Tracker console application. It allows users to create, read, update, and delete coding session. The application saves the coding sessions to a sqlite databased stored on the local client so that they can be accessed later.

## Given Requirements from CodingTracker
- This application has the same requirements as the previous project (HabitTracker), except that now you'll be logging your daily coding time.
- To show the data on the console, you should use the "Spectre.Console" library.
- You're required to have separate classes in different files (ex. UserInput.cs, Validation.cs, CodingController.cs)
- You should tell the user the specific format you want the date and time to be logged and not allow any other format.
- You'll need to create a configuration file that you'll contain your database path and connection strings.
- You'll need to create a "CodingSession" class in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration
- The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate "CalculateDuration" method.
- The user should be able to input the start and end times manually.
- You need to use Dapper ORM for the data access instead of ADO.NET. (This requirement was included in Feb/2024)
- When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.
- Follow the DRY Principle, and avoid code repetition.

## Given Requirements from HabitTracker
- This is an application where you’ll log occurrences of a habit.
- This habit can't be tracked by time (ex. hours of sleep), only by quantity (ex. number of water glasses a day)
- Users need to be able to input the date of the occurrence of the habit
- The application should store and retrieve data from a real database
- When the application starts, it should create a sqlite database, if one isn’t present.
- It should also create a table in the database, where the habit will be logged.
- The users should be able to insert, delete, update and view their logged habit.
- You should handle all possible errors so that the application never crashes.
- You can only interact with the database using ADO.NET. You can’t use mappers such as Entity Framework or Dapper.
- Your project needs to contain a Read Me file where you'll explain how your app works. Here's a nice example:

## How it works
- When the application starts, it checks if the database exists. If it doesn't, it creates a new one.
- The user is then presented with a menu of options to choose from.
- The user can choose to:
    - Add a new Coding Session
    - View all Coding Sessions
    - Update a Coding Session
    - Delete a Coding Session
    - Exit the application
- When adding a new Coding Session, the user is prompted to enter the start and end time.
- When viewing all Coding Session, the user is presented with a list of all Coding Session in the database.
- When updating a Coding Session, the user is prompted to enter all new Coding Session information.
- When deleting a Coding Session, the user is prompted to enter the id of the Coding Session to delete.

## Resources Used
- [SQLite](https://www.sqlite.org/index.html)
- [SQLite Tutorial](https://www.tutorialspoint.com/sqlite/sqlite_c_cpp.htm)
- [SQLite Commands](https://www.sqlitetutorial.net/sqlite-commands/)
- [Spectre.Console](https://spectreconsole.net/)
- [Dapper](https://dapper-tutorial.net/dapper)
- [Using Configuration Manager](https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/store-custom-information-config-file)
- [DateTime in C#](https://medium.com/@Has_San/datetime-in-c-1aef47db4feb)