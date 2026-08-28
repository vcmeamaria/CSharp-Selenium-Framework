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
    public void CountUserByEmail_ShouldIncludeEmail()
    {
        string sql = SqlHelper.CountUserByEmail(
            "peter.parker@test.com"
        );

        Assert.That(
            sql,
            Is.EqualTo(
                "SELECT COUNT(*) FROM Users WHERE Email='peter.parker@test.com';"
            )
        );
    }

    [Test]
    public void InsertUser_ShouldReturnExpectedSql()
    {
        string sql = SqlHelper.InsertUser(
            "Peter Parker",
            "peter.parker@test.com"
        );

        Assert.That(
            sql,
            Is.EqualTo(
                "INSERT INTO Users(Name, Email) VALUES ('Peter Parker', 'peter.parker@test.com');"
            )
        );
    }

    [Test]
    public void UpdateUserName_ShouldReturnExpectedSql()
    {
        string sql = SqlHelper.UpdateUserName(
            "peter.parker@test.com",
            "Peter B Parker"
        );

        Assert.That(
            sql,
            Is.EqualTo(
                "UPDATE Users SET Name='Peter B Parker' WHERE Email='peter.parker@test.com';"
            )
        );
    }

    [Test]
    public void DeleteUserByEmail_ShouldReturnExpectedSql()
    {
        string sql = SqlHelper.DeleteUserByEmail(
            "peter.parker@test.com"
        );

        Assert.That(
            sql,
            Is.EqualTo(
                "DELETE FROM Users WHERE Email='peter.parker@test.com';"
            )
        );
    }
}