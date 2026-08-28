using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SauceDemo.Automation.Config;

namespace SauceDemo.Automation.Core;

public static class DriverFactory
{
    public static IWebDriver Create(TestSettings settings)
    {
        IWebDriver driver =
            settings.Browser.Trim().ToLowerInvariant() switch
            {
                "firefox" =>
                    new FirefoxDriver(
                        CreateFirefoxOptions(settings.Headless)
                    ),

                "edge" =>
                    new EdgeDriver(
                        CreateEdgeOptions(settings.Headless)
                    ),

                _ =>
                    new ChromeDriver(
                        CreateChromeOptions(settings.Headless)
                    )
            };

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

        if (!settings.Headless)
        {
            driver.Manage()
                .Window
                .Maximize();
        }

        return driver;
    }

    private static ChromeOptions CreateChromeOptions(
        bool headless)
    {
        var options = new ChromeOptions();

        if (headless)
        {
            options.AddArgument(
                "--headless=new"
            );
        }

        options.AddArguments(
            "--no-sandbox",
            "--disable-dev-shm-usage",
            "--window-size=1920,1080"
        );

        // Prevent Chrome password-manager popups from
        // interrupting automated tests.
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
            options.AddArgument(
                "-headless"
            );
        }

        return options;
    }

    private static EdgeOptions CreateEdgeOptions(
        bool headless)
    {
        var options = new EdgeOptions();

        if (headless)
        {
            options.AddArgument(
                "--headless=new"
            );
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