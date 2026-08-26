using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using SauceDemo.Automation.Core;
using SauceDemo.Automation.Pages;
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
        var inventory = new LoginPage(DriverContext.Driver).Open(Settings.BaseUrl).LoginAs(Settings.Username,Settings.Password);
        Assert.That(inventory.IsLoaded(),Is.True,"Inventory page was not displayed after login.");
    }
}
