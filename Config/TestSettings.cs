namespace SauceDemo.Automation.Config;

public sealed class TestSettings
{
    public string BaseUrl { get; init; } = "https://www.saucedemo.com/";

    public string ApiBaseUrl { get; init; } = "https://reqres.in/api";

    public string Browser { get; init; } = "chrome";

    public bool Headless { get; init; }

    public int ExplicitWaitSeconds { get; init; } = 10;

    public int PageLoadTimeoutSeconds { get; init; } = 30;

    public string ReportType { get; init; } = "both";

    public string Username { get; init; } = "standard_user";

    public string Password { get; init; } = "secret_sauce";

    public string ScreenshotDirectory { get; init; } =
        "artifacts/screenshots";

    public string ExtentReportDirectory { get; init; } =
        "artifacts/extent";

    public string LogDirectory { get; init; } =
        "artifacts/logs";
}