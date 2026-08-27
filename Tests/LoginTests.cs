using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using SauceDemo.Automation.Core;
using SauceDemo.Automation.Pages;
using SauceDemo.Automation.Utilities;

namespace SauceDemo.Automation.Tests;

[AllureNUnit]
[AllureEpic("SauceDemo Web UI")]
public sealed class LoginTests : BaseTest
{
    [Test]
    [Category("Smoke")]
    [AllureFeature("Authentication")]
    [AllureStory("Valid login")]
    public void ValidUserCanLogin()
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

        var inventory = new LoginPage(DriverContext.Driver)
            .Open(Settings.BaseUrl)
            .LoginAs(username, password);

        Assert.That(
            inventory.IsLoaded(),
            Is.True,
            "Inventory page was not displayed after login."
        );
    }
}