using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
namespace SauceDemo.Automation.Pages;
public sealed class LoginPage : BasePage
{
    [FindsBy(How = How.Id, Using = "user-name")] private IWebElement? Username { get; set; }
    [FindsBy(How = How.Id, Using = "password")] private IWebElement? Password { get; set; }
    [FindsBy(How = How.Id, Using = "login-button")] private IWebElement? LoginButton { get; set; }
    public LoginPage(IWebDriver driver) : base(driver) => PageFactory.InitElements(driver, this);
    public LoginPage Open(string url){ Driver.Navigate().GoToUrl(url); return this; }
    public InventoryPage LoginAs(string username,string password){ Username!.Clear(); Username.SendKeys(username); Password!.Clear(); Password.SendKeys(password); LoginButton!.Click(); return new InventoryPage(Driver); }
}
