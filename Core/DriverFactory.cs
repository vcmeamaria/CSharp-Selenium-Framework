using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using SauceDemo.Automation.Config;

namespace SauceDemo.Automation.Core;

public static class DriverFactory
{
    public static IWebDriver Create(TestSettings settings)
    {
        string? gridUrl =
            Environment.GetEnvironmentVariable(
                "SELENIUM_GRID_URL"
            );

        IWebDriver driver;

        if (!string.IsNullOrWhiteSpace(gridUrl))
        {
            driver = CreateRemoteDriver(
                settings,
                gridUrl
            );
        }
        else
        {
            driver = CreateLocalDriver(settings);
        }

        driver.Manage()
            .Timeouts()
            .PageLoad =
            TimeSpan.FromSeconds(
                settings.PageLoadTimeoutSeconds
            );

        driver.Manage()
            .Timeouts()
            .ImplicitWait =
            TimeSpan.Zero;

        if (
            string.IsNullOrWhiteSpace(gridUrl)
            && !settings.Headless
        )
        {
            driver.Manage().Window.Maximize();
        }

        return driver;
    }

    private static IWebDriver CreateLocalDriver(
        TestSettings settings)
    {
        return settings.Browser
            .Trim()
            .ToLowerInvariant() switch
        {
            "firefox" =>
                new FirefoxDriver(
                    CreateFirefoxOptions(
                        settings.Headless
                    )
                ),

            "edge" =>
                new EdgeDriver(
                    CreateEdgeOptions(
                        settings.Headless
                    )
                ),

            _ =>
                new ChromeDriver(
                    CreateChromeOptions(
                        settings.Headless
                    )
                )
        };
    }

    private static IWebDriver CreateRemoteDriver(
        TestSettings settings,
        string gridUrl)
    {
        DriverOptions options =
            settings.Browser
                .Trim()
                .ToLowerInvariant() switch
            {
                "firefox" =>
                    CreateFirefoxOptions(
                        settings.Headless
                    ),

                "chrome" =>
                    CreateChromeOptions(
                        settings.Headless
                    ),

                _ =>
                    throw new NotSupportedException(
                        $"Browser '{settings.Browser}' " +
                        "is not configured in Selenium Grid."
                    )
            };

        return new RemoteWebDriver(
            new Uri(gridUrl),
            options.ToCapabilities(),
            TimeSpan.FromSeconds(
                settings.PageLoadTimeoutSeconds
            )
        );
    }

    private static ChromeOptions CreateChromeOptions(
        bool headless)
    {
        var options = new ChromeOptions();

        if (headless)
        {
            options.AddArgument("--headless=new");
        }

        options.AddArguments(
            "--no-sandbox",
            "--disable-dev-shm-usage",
            "--window-size=1920,1080"
        );

        options.AddUserProfilePreference(
            "credentials_enable_service",
            false
        );

        options.AddUserProfilePreference(
            "profile.password_manager_enabled",
            false
        );

        options.AddUserProfilePreference(
            "profile.password_manager_leak_detection",
            false
        );

        return options;
    }

    private static FirefoxOptions CreateFirefoxOptions(
        bool headless)
    {
        var options = new FirefoxOptions();

        if (headless)
        {
            options.AddArgument("-headless");
        }

        return options;
    }

    private static EdgeOptions CreateEdgeOptions(
        bool headless)
    {
        var options = new EdgeOptions();

        if (headless)
        {
            options.AddArgument("--headless=new");
        }

        options.AddArguments(
            "--no-sandbox",
            "--disable-dev-shm-usage",
            "--window-size=1920,1080"
        );

        options.AddUserProfilePreference(
            "credentials_enable_service",
            false
        );

        options.AddUserProfilePreference(
            "profile.password_manager_enabled",
            false
        );

        options.AddUserProfilePreference(
            "profile.password_manager_leak_detection",
            false
        );

        return options;
    }
}