using NUnit.Framework;
using SauceDemo.Automation.Config;
using SauceDemo.Automation.Utilities;

namespace SauceDemo.Automation.Tests;

public sealed class ApiTests
{
    [Test]
    [Category("API")]
    public async Task GetUser_ShouldReturn200()
    {
        var api = new ApiClient(
            ConfigurationManager.Settings.ApiBaseUrl
        );

        var response = await api.GetAsync(
            "/users/2"
        );

        Assert.That(
            (int)response.StatusCode,
            Is.EqualTo(200),
            $"Expected status code 200 but received {(int)response.StatusCode}."
        );
    }
}