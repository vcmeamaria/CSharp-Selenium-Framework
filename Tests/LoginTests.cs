using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using SauceDemo.Automation.Core;
using SauceDemo.Automation.Listeners;
using SauceDemo.Automation.Pages;
using SauceDemo.Automation.Utilities;

namespace SauceDemo.Automation.Tests;

[AllureNUnit]
[AllureEpic("SauceDemo Web UI")]
[TestListener]
public sealed class LoginTests : BaseTest
{
    [Test]
    [Category("Smoke")]
    [AllureFeature("Authentication")]
    [AllureStory("Valid login using JSON data")]
    public void ValidUserCanLoginUsingJsonData()
    {
        string username = JsonReader.GetValue(
            "loginData.json",
            "validUser",
            "username"
        );

        string password = JsonReader.GetValue(
            "loginData.json",
            "validUser",
            "password"
        );

        var inventory = new LoginPage(
            DriverContext.Driver
        )
        .Open(Settings.BaseUrl)
        .LoginAs(username, password);

        Assert.That(
            inventory.IsLoaded(),
            Is.True,
            "Inventory page was not displayed after login."
        );
    }

    [Test]
    [Category("Smoke")]
    [AllureFeature("Authentication")]
    [AllureStory("Valid login using Excel data")]
    public void ValidUserCanLoginUsingExcelData()
    {
        string username = ExcelReader.GetValue(
            "loginData.xlsx",
            "LoginData",
            2,
            1
        );

        string password = ExcelReader.GetValue(
            "loginData.xlsx",
            "LoginData",
            2,
            2
        );

        var inventory = new LoginPage(
            DriverContext.Driver
        )
        .Open(Settings.BaseUrl)
        .LoginAs(username, password);

        Assert.That(
            inventory.IsLoaded(),
            Is.True,
            "Inventory page was not displayed after login."
        );
    }
}