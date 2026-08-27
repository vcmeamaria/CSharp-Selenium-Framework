using Microsoft.Extensions.Configuration;

namespace SauceDemo.Automation.Config;

public static class ConfigurationManager
{
    private static readonly Lazy<TestSettings> LazySettings =
        new(Load);

    public static TestSettings Settings => LazySettings.Value;

    private static TestSettings Load()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(
                "appsettings.json",
                optional: false
            )
            .AddEnvironmentVariables(prefix: "SAUCE_")
            .Build();

        var settings = configuration
            .GetSection("TestSettings")
            .Get<TestSettings>()
            ?? new TestSettings();

        return settings.WithEnvironmentOverrides();
    }

    private static TestSettings WithEnvironmentOverrides(
        this TestSettings settings)
    {
        return new TestSettings
        {
            BaseUrl =
                Environment.GetEnvironmentVariable(
                    "SAUCE_BASE_URL"
                )
                ?? settings.BaseUrl,

            ApiBaseUrl =
                Environment.GetEnvironmentVariable(
                    "SAUCE_API_BASE_URL"
                )
                ?? settings.ApiBaseUrl,

            Browser =
                Environment.GetEnvironmentVariable(
                    "SAUCE_BROWSER"
                )
                ?? settings.Browser,

            Headless =
                bool.TryParse(
                    Environment.GetEnvironmentVariable(
                        "SAUCE_HEADLESS"
                    ),
                    out bool headless
                )
                ? headless
                : settings.Headless,

            ExplicitWaitSeconds =
                int.TryParse(
                    Environment.GetEnvironmentVariable(
                        "SAUCE_EXPLICIT_WAIT_SECONDS"
                    ),
                    out int explicitWait
                )
                ? explicitWait
                : settings.ExplicitWaitSeconds,

            PageLoadTimeoutSeconds =
                int.TryParse(
                    Environment.GetEnvironmentVariable(
                        "SAUCE_PAGE_LOAD_TIMEOUT_SECONDS"
                    ),
                    out int pageLoadTimeout
                )
                ? pageLoadTimeout
                : settings.PageLoadTimeoutSeconds,

            ReportType =
                Environment.GetEnvironmentVariable(
                    "SAUCE_REPORT_TYPE"
                )
                ?? settings.ReportType,

            Username =
                Environment.GetEnvironmentVariable(
                    "SAUCE_USERNAME"
                )
                ?? settings.Username,

            Password =
                Environment.GetEnvironmentVariable(
                    "SAUCE_PASSWORD"
                )
                ?? settings.Password,

            ScreenshotDirectory =
                settings.ScreenshotDirectory,

            ExtentReportDirectory =
                settings.ExtentReportDirectory,

            LogDirectory =
                settings.LogDirectory
        };
    }
}