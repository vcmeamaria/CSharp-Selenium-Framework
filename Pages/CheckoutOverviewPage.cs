using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Automation.Pages;

public sealed class CheckoutOverviewPage : BasePage
{
    [FindsBy(
        How = How.CssSelector,
        Using = ".title"
    )]
    private IWebElement? Title { get; set; }

    [FindsBy(
        How = How.Id,
        Using = "finish"
    )]
    private IWebElement? FinishButton { get; set; }

    public CheckoutOverviewPage(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    public bool IsLoaded()
    {
        Wait.UrlContains(
            "checkout-step-two.html"
        );

        return Title!.Displayed &&
               Title.Text == "Checkout: Overview";
    }

    public bool ContainsProduct(
        string productName)
    {
        IReadOnlyCollection<IWebElement> products =
            Driver.FindElements(
                By.CssSelector(
                    ".inventory_item_name"
                )
            );

        return products.Any(
            product =>
                product.Text == productName
        );
    }

    public CheckoutCompletePage FinishOrder()
    {
        FinishButton!.Click();

        return new CheckoutCompletePage(
            Driver
        );
    }
}