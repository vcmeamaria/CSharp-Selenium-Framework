using OpenQA.Selenium;
namespace SauceDemo.Automation.Utilities;
public static class ScreenshotUtils
{
    public static string Capture(IWebDriver driver, string directory, string testName)
    {
        Directory.CreateDirectory(directory);
        var safe = string.Concat(testName.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
        var path = Path.GetFullPath(Path.Combine(directory, $"{safe}_{DateTime.Now:yyyyMMdd_HHmmssfff}.png"));
        ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
        return path;
    }
}
