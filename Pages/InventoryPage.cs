using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
namespace SauceDemo.Automation.Pages;
public sealed class InventoryPage : BasePage
{
    [FindsBy(How = How.CssSelector, Using = ".title")] private IWebElement? Title { get; set; }
    public InventoryPage(IWebDriver driver) : base(driver) => PageFactory.InitElements(driver, this);
    public bool IsLoaded(){ Wait.UrlContains("inventory.html"); return Title!.Displayed && Title.Text == "Products"; }
}
