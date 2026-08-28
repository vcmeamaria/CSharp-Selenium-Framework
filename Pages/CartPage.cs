using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Automation.Pages;

public sealed class CartPage : BasePage
{
    [FindsBy(
        How = How.CssSelector,
        Using = ".title"
    )]
    private IWebElement? Title { get; set; }

    [FindsBy(
        How = How.Id,
        Using = "checkout"
    )]
    private IWebElement? CheckoutButton { get; set; }

    public CartPage(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    public bool IsLoaded()
    {
        Wait.UrlContains("cart.html");

        return Title!.Displayed &&
               Title.Text == "Your Cart";
    }

    public bool ContainsProduct(string productName)
    {
        IReadOnlyCollection<IWebElement> products =
            Driver.FindElements(
                By.CssSelector(".inventory_item_name")
            );

        return products.Any(
            product => product.Text == productName
        );
    }

    public CheckoutPage Checkout()
    {
        CheckoutButton!.Click();

        return new CheckoutPage(Driver);
    }
}