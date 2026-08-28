using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Automation.Pages;

public sealed class CheckoutPage : BasePage
{
    [FindsBy(
        How = How.CssSelector,
        Using = ".title"
    )]
    private IWebElement? Title { get; set; }

    [FindsBy(
        How = How.Id,
        Using = "first-name"
    )]
    private IWebElement? FirstName { get; set; }

    [FindsBy(
        How = How.Id,
        Using = "last-name"
    )]
    private IWebElement? LastName { get; set; }

    [FindsBy(
        How = How.Id,
        Using = "postal-code"
    )]
    private IWebElement? PostalCode { get; set; }

    [FindsBy(
        How = How.Id,
        Using = "continue"
    )]
    private IWebElement? ContinueButton { get; set; }

    public CheckoutPage(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    public bool IsLoaded()
    {
        Wait.UrlContains(
            "checkout-step-one.html"
        );

        return Title!.Displayed &&
               Title.Text == "Checkout: Your Information";
    }

    public CheckoutOverviewPage EnterCustomerDetails(
        string firstName,
        string lastName,
        string postalCode)
    {
        FirstName!.Clear();
        FirstName.SendKeys(firstName);

        LastName!.Clear();
        LastName.SendKeys(lastName);

        PostalCode!.Clear();
        PostalCode.SendKeys(postalCode);

        ContinueButton!.Click();

        return new CheckoutOverviewPage(
            Driver
        );
    }
}