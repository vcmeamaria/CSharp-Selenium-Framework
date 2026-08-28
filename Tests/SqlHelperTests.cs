using NUnit.Framework;
using SauceDemo.Automation.Utilities;

namespace SauceDemo.Automation.Tests;

public sealed class SqlHelperTests
{
    [Test]
    public void CountAllUsers_ShouldReturnExpectedSql()
    {
        string sql = SqlHelper.CountAllUsers();

        Assert.That(
            sql,
            Is.EqualTo("SELECT COUNT(*) FROM Users;")
        );
    }

    [Test]
    public void CountUserByEmail_ShouldUseEmailParameter()
    {
        string sql = SqlHelper.CountUserByEmail();

        Assert.That(
            sql,
            Is.EqualTo(
                "SELECT COUNT(*) FROM Users WHERE Email=@Email;"
            )
        );
    }

    [Test]
    public void InsertUser_ShouldUseParameters()
    {
        string sql = SqlHelper.InsertUser();

        Assert.That(
            sql,
            Is.EqualTo(
                "INSERT INTO Users(Name, Email) VALUES (@Name, @Email);"
            )
        );
    }

    [Test]
    public void UpdateUserName_ShouldUseParameters()
    {
        string sql = SqlHelper.UpdateUserName();

        Assert.That(
            sql,
            Is.EqualTo(
                "UPDATE Users SET Name=@Name WHERE Email=@Email;"
            )
        );
    }

    [Test]
    public void DeleteUserByEmail_ShouldUseEmailParameter()
    {
        string sql = SqlHelper.DeleteUserByEmail();

        Assert.That(
            sql,
            Is.EqualTo(
                "DELETE FROM Users WHERE Email=@Email;"
            )
        );
    }
}