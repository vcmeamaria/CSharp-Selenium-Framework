using OpenQA.Selenium;
using SauceDemo.Automation.Config;
using SauceDemo.Automation.Utilities;
namespace SauceDemo.Automation.Pages;
public abstract class BasePage
{
    protected IWebDriver Driver { get; }
    protected WaitUtils Wait { get; }
    protected BasePage(IWebDriver driver) { Driver=driver; Wait=new WaitUtils(driver, ConfigurationManager.Settings.ExplicitWaitSeconds); }
    protected void Type(By locator,string text){ var e=Wait.Visible(locator); e.Clear(); e.SendKeys(text); }
    protected void Click(By locator)=>Wait.Visible(locator).Click();
}
