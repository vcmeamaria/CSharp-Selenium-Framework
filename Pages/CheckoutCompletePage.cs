using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Automation.Pages;

public sealed class CheckoutCompletePage : BasePage
{
    [FindsBy(
        How = How.CssSelector,
        Using = ".title"
    )]
    private IWebElement? Title { get; set; }

    [FindsBy(
        How = How.CssSelector,
        Using = ".complete-header"
    )]
    private IWebElement? ConfirmationMessage { get; set; }

    public CheckoutCompletePage(
        IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(
            driver,
            this
        );
    }

    public bool IsLoaded()
    {
        Wait.UrlContains(
            "checkout-complete.html"
        );

        return Title!.Displayed &&
               Title.Text == "Checkout: Complete!";
    }

    public string GetConfirmationMessage()
    {
        return ConfirmationMessage!.Text;
    }
}