using Microsoft.Data.SqlClient;

namespace SauceDemo.Automation.Utilities;

public static class DatabaseHelper
{
    public static object? ExecuteScalar(
        string connectionString,
        string query)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException(
                "Database connection string cannot be empty.",
                nameof(connectionString)
            );
        }

        if (string.IsNullOrWhiteSpace(query))
        {
            throw new ArgumentException(
                "SQL query cannot be empty.",
                nameof(query)
            );
        }

        using var connection = new SqlConnection(connectionString);

        using var command = new SqlCommand(
            query,
            connection
        );

        connection.Open();

        return command.ExecuteScalar();
    }
}