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

    public static string CountUserByEmail()
    {
        return "SELECT COUNT(*) FROM Users WHERE Email=@Email;";
    }

    public static string InsertUser()
    {
        return
            "INSERT INTO Users(Name, Email) " +
            "VALUES (@Name, @Email);";
    }

    public static string UpdateUserName()
    {
        return
            "UPDATE Users " +
            "SET Name=@Name " +
            "WHERE Email=@Email;";
    }

    public static string DeleteUserByEmail()
    {
        return
            "DELETE FROM Users " +
            "WHERE Email=@Email;";
    }
}