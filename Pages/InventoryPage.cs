using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Automation.Pages;

public sealed class InventoryPage : BasePage
{
    [FindsBy(How = How.CssSelector, Using = ".title")]
    private IWebElement? Title { get; set; }

    [FindsBy(
        How = How.CssSelector,
        Using = "[data-test='product-sort-container']"
    )]
    private IWebElement? SortDropdown { get; set; }

    [FindsBy(
        How = How.Id,
        Using = "add-to-cart-sauce-labs-backpack"
    )]
    private IWebElement? AddBackpackButton { get; set; }

    [FindsBy(
        How = How.CssSelector,
        Using = ".shopping_cart_link"
    )]
    private IWebElement? ShoppingCartLink { get; set; }

    public InventoryPage(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    public bool IsLoaded()
    {
        Wait.UrlContains("inventory.html");

        return Title!.Displayed &&
               Title.Text == "Products";
    }

    public InventoryPage SortByPriceLowToHigh()
    {
        var select = new SelectElement(
            SortDropdown!
        );

        select.SelectByValue("lohi");

        return this;
    }

    public List<decimal> GetProductPrices()
    {
        IReadOnlyCollection<IWebElement> priceElements =
            Driver.FindElements(
                By.CssSelector(".inventory_item_price")
            );

        return priceElements
            .Select(element =>
                decimal.Parse(
                    element.Text.Replace("$", ""),
                    CultureInfo.InvariantCulture
                )
            )
            .ToList();
    }

    public InventoryPage AddSauceLabsBackpackToCart()
    {
        AddBackpackButton!.Click();

        return this;
    }

    public CartPage OpenCart()
    {
        ShoppingCartLink!.Click();

        return new CartPage(Driver);
    }
}