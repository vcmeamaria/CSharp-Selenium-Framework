using OpenQA.Selenium;
namespace SauceDemo.Automation.Core;
public static class DriverContext
{
    private static readonly AsyncLocal<IWebDriver?> CurrentDriver = new();
    public static IWebDriver Driver => CurrentDriver.Value ?? throw new InvalidOperationException("WebDriver has not been initialised.");
    public static void Set(IWebDriver driver) => CurrentDriver.Value = driver;
    public static void Quit()
    {
        try { CurrentDriver.Value?.Quit(); }
        finally { CurrentDriver.Value?.Dispose(); CurrentDriver.Value = null; }
    }
}
