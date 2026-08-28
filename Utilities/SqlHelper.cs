namespace SauceDemo.Automation.Utilities;

public static class SqlHelper
{
    public static string SelectAllUsers()
    {
        return "SELECT * FROM Users;";
    }

    public static string CountAllUsers()
    {
        return "SELECT COUNT(*) FROM Users;";
    }

    public static string CountUserByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException(
                "Email cannot be empty.",
                nameof(email)
            );
        }

        return $"SELECT COUNT(*) FROM Users WHERE Email='{email}';";
    }

    public static string InsertUser(
        string name,
        string email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Name cannot be empty.",
                nameof(name)
            );
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException(
                "Email cannot be empty.",
                nameof(email)
            );
        }

        return
            $"INSERT INTO Users(Name, Email) " +
            $"VALUES ('{name}', '{email}');";
    }

    public static string UpdateUserName(
        string email,
        string newName)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException(
                "Email cannot be empty.",
                nameof(email)
            );
        }

        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException(
                "New name cannot be empty.",
                nameof(newName)
            );
        }

        return
            $"UPDATE Users " +
            $"SET Name='{newName}' " +
            $"WHERE Email='{email}';";
    }

    public static string DeleteUserByEmail(
        string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException(
                "Email cannot be empty.",
                nameof(email)
            );
        }

        return
            $"DELETE FROM Users " +
            $"WHERE Email='{email}';";
    }
}