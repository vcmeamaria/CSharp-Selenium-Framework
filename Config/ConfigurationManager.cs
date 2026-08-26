using Microsoft.Extensions.Configuration;
namespace SauceDemo.Automation.Config;
public static class ConfigurationManager
{
    private static readonly Lazy<TestSettings> LazySettings = new(Load);
    public static TestSettings Settings => LazySettings.Value;
    private static TestSettings Load()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables(prefix: "SAUCE_")
            .Build();
        var settings = configuration.GetSection("TestSettings").Get<TestSettings>() ?? new TestSettings();
        return settings.withEnvironmentOverrides();
    }
    private static TestSettings withEnvironmentOverrides(this TestSettings s) => new()
    {
        BaseUrl = Environment.GetEnvironmentVariable("SAUCE_BASE_URL") ?? s.BaseUrl,
        Browser = Environment.GetEnvironmentVariable("SAUCE_BROWSER") ?? s.Browser,
        Headless = bool.TryParse(Environment.GetEnvironmentVariable("SAUCE_HEADLESS"), out var h) ? h : s.Headless,
        ExplicitWaitSeconds = int.TryParse(Environment.GetEnvironmentVariable("SAUCE_EXPLICIT_WAIT_SECONDS"), out var e) ? e : s.ExplicitWaitSeconds,
        PageLoadTimeoutSeconds = int.TryParse(Environment.GetEnvironmentVariable("SAUCE_PAGE_LOAD_TIMEOUT_SECONDS"), out var p) ? p : s.PageLoadTimeoutSeconds,
        ReportType = Environment.GetEnvironmentVariable("SAUCE_REPORT_TYPE") ?? s.ReportType,
        Username = Environment.GetEnvironmentVariable("SAUCE_USERNAME") ?? s.Username,
        Password = Environment.GetEnvironmentVariable("SAUCE_PASSWORD") ?? s.Password,
        ScreenshotDirectory = s.ScreenshotDirectory,
        ExtentReportDirectory = s.ExtentReportDirectory,
        LogDirectory = s.LogDirectory
    };
}
