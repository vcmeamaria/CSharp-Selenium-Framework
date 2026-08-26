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
        IWebDriver driver = settings.Browser.Trim().ToLowerInvariant() switch
        {
            "firefox" => new FirefoxDriver(FirefoxOptions(settings.Headless)),
            "edge" => new EdgeDriver(EdgeOptions(settings.Headless)),
            _ => new ChromeDriver(ChromeOptions(settings.Headless))
        };
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(settings.PageLoadTimeoutSeconds);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        if (!settings.Headless) driver.Manage().Window.Maximize();
        return driver;
    }
    private static ChromeOptions ChromeOptions(bool headless) { var o = new ChromeOptions(); if(headless)o.AddArgument("--headless=new"); o.AddArguments("--no-sandbox","--disable-dev-shm-usage","--window-size=1920,1080"); return o; }
    private static FirefoxOptions FirefoxOptions(bool headless) { var o = new FirefoxOptions(); if(headless)o.AddArgument("-headless"); return o; }
    private static EdgeOptions EdgeOptions(bool headless) { var o = new EdgeOptions(); if(headless)o.AddArgument("--headless=new"); o.AddArguments("--no-sandbox","--disable-dev-shm-usage","--window-size=1920,1080"); return o; }
}
