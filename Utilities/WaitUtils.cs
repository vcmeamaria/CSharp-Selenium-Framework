using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace SauceDemo.Automation.Utilities;
public sealed class WaitUtils(IWebDriver driver, int seconds)
{
    private readonly WebDriverWait _wait = new(driver, TimeSpan.FromSeconds(seconds));
    public IWebElement Visible(By locator) => _wait.Until(d => { try { var e=d.FindElement(locator); return e.Displayed ? e : null; } catch(NoSuchElementException){ return null; } catch(StaleElementReferenceException){ return null; } })!;
    public void UrlContains(string value) => _wait.Until(d => d.Url.Contains(value, StringComparison.OrdinalIgnoreCase));
}
