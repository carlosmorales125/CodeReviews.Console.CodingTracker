using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;

namespace CodingTracker;

public class DbContext(IConfiguration configuration)
{
    private readonly string _connectionString = configuration.GetConnectionString("CodingTracker");
    
    public void CreateDatabase()
    {
        var connection = new SqliteConnection(_connectionString);
        
        try
        {
            connection.Open();
            var createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS CodingTracker (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime INTEGER NOT NULL,
                        EndTime INTEGER NOT NULL,
                        Duration INTEGER NOT NULL
                    )";
            connection.Execute(createTableQuery);
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
    }
    
    public void AddCodingSession(CodingSession codingSession)
    {
        var connection = new SqliteConnection(_connectionString);
        
        try
        {
            connection.Open();
            var addCodingSessionQuery = @"
                INSERT INTO CodingTracker (StartTime, EndTime, Duration)
                VALUES (@StartTime, @EndTime, @Duration)";
            connection.Execute(addCodingSessionQuery, new { codingSession.StartTime, codingSession.EndTime, codingSession.Duration });
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
    }
}