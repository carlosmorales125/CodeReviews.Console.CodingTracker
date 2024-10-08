using CodingTracker.Models;
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
                        EndTime INTEGER NOT NULL
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
    
    public ActionMessage AddCodingSession(CodingSession codingSession)
    {
        var connection = new SqliteConnection(_connectionString);
        
        try
        {
            connection.Open();
            var addCodingSessionQuery = @"
                INSERT INTO CodingTracker (StartTime, EndTime)
                VALUES (@StartTime, @EndTime)";
            connection.Execute(addCodingSessionQuery, new { codingSession.StartTime, codingSession.EndTime});
        }
        catch (SqliteException ex)
        {
            return new ActionMessage()
            {
                Message = $"SQLite error: {ex.Message}",
                IsError = true
            };
        }
        finally
        {
            connection.Close();
        }
        
        return new ActionMessage()
        {
            Message = "Successfully added Coding Session!",
            IsError = false
        };
    }

    public List<CodingSession> GetCodingSessions()
    {
        var connection = new SqliteConnection(_connectionString);
        
        try
        {
            connection.Open();
            var getCodingSessionsQuery = "SELECT * FROM CodingTracker";
            return connection.Query<CodingSession>(getCodingSessionsQuery).ToList();
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
            return new List<CodingSession>();
        }
        finally
        {
            connection.Close();
        }
    }
    
    public ActionMessage DeleteCodingSession(int id)
    {
        var connection = new SqliteConnection(_connectionString);
        
        try
        {
            connection.Open();
            var getCodingSessionsQuery = "SELECT * FROM CodingTracker WHERE Id = @Id";
            var codingSession = connection.QueryFirstOrDefault<CodingSession>(getCodingSessionsQuery, new { Id = id });
            
            if (codingSession == null)
            {
                return new ActionMessage()
                {
                    Message = "Coding session not found",
                    IsError = true
                };
            }
            
            var deleteCodingSessionQuery = "DELETE FROM CodingTracker WHERE Id = @Id";
            connection.Execute(deleteCodingSessionQuery, new { Id = id });
        }
        catch (SqliteException ex)
        { 
            return new ActionMessage()
            {
                Message = $"SQLite error: {ex.Message}",
                IsError = true
            };
        }
        finally
        {
            connection.Close();
        }
        
        return new ActionMessage()
        {
            Message = "Coding session deleted",
            IsError = false
        };
    }
    
    public ActionMessage EditCodingSession(CodingSession codingSession)
    {
        var connection = new SqliteConnection(_connectionString);
        
        try
        {
            connection.Open();
            var editCodingSessionQuery = @"
                UPDATE CodingTracker
                SET StartTime = @StartTime, EndTime = @EndTime
                WHERE Id = @Id";
            connection.Execute(editCodingSessionQuery, new { codingSession.StartTime, codingSession.EndTime, codingSession.Id });
        }
        catch (SqliteException ex)
        {
            return new ActionMessage()
            {
                Message = $"SQLite error: {ex.Message}",
                IsError = true
            };
        }
        finally
        {
            connection.Close();
        }
        
        return new ActionMessage()
        {
            Message = "Successfully edited Coding Session!",
            IsError = false
        };
    }

    public ActionMessage CheckIfIdExist(int id)
    {
        var connection = new SqliteConnection(_connectionString);
        
        try
        {
            connection.Open();
            var getCodingSessionsQuery = "SELECT * FROM CodingTracker WHERE Id = @Id";
            var existingCodingSession = connection.QueryFirstOrDefault<CodingSession>(getCodingSessionsQuery, new { Id = id });
            if (existingCodingSession == null)
            {
                return new ActionMessage()
                {
                    Message = "Coding session not found",
                    IsError = true
                };
            }
        }
        catch (SqliteException ex)
        {
            return new ActionMessage()
            {
                Message = $"SQLite error: {ex.Message}",
                IsError = true
            };
        }
        finally
        {
            connection.Close();
        }
        return new ActionMessage()
        {
            Message = "Coding session found",
            IsError = false
        };
    }
}
