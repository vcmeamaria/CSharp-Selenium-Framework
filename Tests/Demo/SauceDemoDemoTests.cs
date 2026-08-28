using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using SauceDemo.Automation.Core;
using SauceDemo.Automation.Listeners;
using SauceDemo.Automation.Pages;

namespace SauceDemo.Automation.Tests.Demo;

[AllureNUnit]
[AllureEpic("SauceDemo Demo Test Suite")]
[TestListener]
public sealed class SauceDemoDemoTests : BaseTest
{
    // =========================================================
    // TC_LOGIN_001
    // Type: Functional / Positive
    // Priority: High
    //
    // Scenario:
    // Verify that a valid user can log in successfully.
    //
    // Test Data:
    // Username: standard_user
    // Password: secret_sauce
    //
    // Expected Result:
    // The inventory/products page is displayed.
    // =========================================================

    [Test]
    [Category("Demo")]
    [Category("Functional")]
    [AllureFeature("Authentication")]
    [AllureStory("TC_LOGIN_001 - Valid Login")]
    public void TC_LOGIN_001_ValidUserCanLogin()
    {
        const string username = "standard_user";
        const string password = "secret_sauce";

        var inventory = new LoginPage(
            DriverContext.Driver
        )
        .Open(Settings.BaseUrl)
        .LoginAs(username, password);

        Assert.That(
            inventory.IsLoaded(),
            Is.True,
            "Inventory page should be displayed after a successful login."
        );
    }

    // =========================================================
    // TC_LOGIN_002
    // Type: Functional / Negative
    // Priority: High
    //
    // Scenario:
    // Verify that login is rejected when an invalid password
    // is entered for a valid username.
    //
    // Test Data:
    // Username: standard_user
    // Password: wrong_password
    //
    // Expected Result:
    // Login is blocked and an error message is displayed.
    // =========================================================

    [Test]
    [Category("Demo")]
    [Category("Negative")]
    [AllureFeature("Authentication")]
    [AllureStory("TC_LOGIN_002 - Invalid Password")]
    public void TC_LOGIN_002_InvalidPasswordShowsError()
    {
        const string username = "standard_user";
        const string password = "wrong_password";

        var loginPage = new LoginPage(
            DriverContext.Driver
        )
        .Open(Settings.BaseUrl)
        .LoginExpectingFailure(
            username,
            password
        );

        string errorMessage =
            loginPage.GetErrorMessage();

        Assert.That(
            errorMessage,
            Does.Contain(
                "Username and password do not match"
            ),
            "An invalid password should display a login error."
        );
    }

    // =========================================================
    // TC_SECURITY_001
    // Type: Security / Negative
    // Priority: High
    //
    // Scenario:
    // Verify that SQL-injection-style input does not bypass
    // the login authentication process.
    //
    // Test Data:
    // Username: ' OR 1=1 --
    // Password: test
    //
    // Expected Result:
    // Login is blocked and an error message is displayed.
    // =========================================================

    [Test]
    [Category("Demo")]
    [Category("Security")]
    [AllureFeature("Authentication Security")]
    [AllureStory("TC_SECURITY_001 - SQL Injection Login Attempt")]
    public void TC_SECURITY_001_SqlInjectionAttemptIsBlocked()
    {
        const string username = "' OR 1=1 --";
        const string password = "test";

        var loginPage = new LoginPage(
            DriverContext.Driver
        )
        .Open(Settings.BaseUrl)
        .LoginExpectingFailure(
            username,
            password
        );

        string errorMessage =
            loginPage.GetErrorMessage();

        Assert.That(
            errorMessage,
            Is.Not.Empty,
            "SQL-injection-style input should not allow authentication."
        );

        Assert.That(
            DriverContext.Driver.Url,
            Does.Not.Contain("inventory"),
            "SQL-injection-style input must not bypass authentication."
        );
    }

    // =========================================================
    // TC_SORT_001
    // Type: Functional / Usability
    // Priority: Medium
    //
    // Scenario:
    // Verify that products can be sorted by price
    // from low to high.
    //
    // Test Data:
    // Valid SauceDemo user
    // Sort option: Price (low to high)
    //
    // Expected Result:
    // All product prices are displayed in ascending order.
    // =========================================================

    [Test]
    [Category("Demo")]
    [Category("Functional")]
    [Category("Usability")]
    [AllureFeature("Product Sorting")]
    [AllureStory("TC_SORT_001 - Price Low to High")]
    public void TC_SORT_001_ProductsSortByPriceLowToHigh()
    {
        var inventory = new LoginPage(
            DriverContext.Driver
        )
        .Open(Settings.BaseUrl)
        .LoginAs(
            "standard_user",
            "secret_sauce"
        );

        Assert.That(
            inventory.IsLoaded(),
            Is.True,
            "Inventory page should be displayed before testing sorting."
        );

        inventory.SortByPriceLowToHigh();

        List<decimal> actualPrices =
            inventory.GetProductPrices();

        List<decimal> expectedPrices =
            actualPrices
                .OrderBy(price => price)
                .ToList();

        Assert.That(
            actualPrices.Count,
            Is.GreaterThan(1),
            "Multiple products should be available for sorting."
        );

        Assert.That(
            actualPrices,
            Is.EqualTo(expectedPrices),
            "Products should be ordered from the lowest price to the highest price."
        );
    }

    // =========================================================
    // TC_E2E_001
    // Type: End-to-End / Integration
    // Priority: Critical
    //
    // Scenario:
    // Verify the complete purchase journey from login
    // through to successful order confirmation.
    //
    // Test Data:
    // Username: standard_user
    // Password: secret_sauce
    // Product: Sauce Labs Backpack
    // First Name: Peter
    // Last Name: Parker
    // Postal Code: CV1 1AA
    //
    // Expected Result:
    // The order is completed successfully and the confirmation
    // message "Thank you for your order!" is displayed.
    // =========================================================

    [Test]
    [Category("Demo")]
    [Category("EndToEnd")]
    [Category("Integration")]
    [AllureFeature("Checkout")]
    [AllureStory("TC_E2E_001 - Complete Checkout Journey")]
    public void TC_E2E_001_UserCanCompleteCheckout()
    {
        const string productName =
            "Sauce Labs Backpack";

        // Step 1: Login.
        var inventory = new LoginPage(
            DriverContext.Driver
        )
        .Open(Settings.BaseUrl)
        .LoginAs(
            "standard_user",
            "secret_sauce"
        );

        Assert.That(
            inventory.IsLoaded(),
            Is.True,
            "Inventory page should be displayed after login."
        );

        // Step 2: Add a product to the cart.
        inventory.AddSauceLabsBackpackToCart();

        // Step 3: Open the cart.
        CartPage cart =
            inventory.OpenCart();

        Assert.That(
            cart.IsLoaded(),
            Is.True,
            "Cart page should be displayed."
        );

        Assert.That(
            cart.ContainsProduct(productName),
            Is.True,
            $"{productName} should be displayed in the cart."
        );

        // Step 4: Begin checkout.
        CheckoutPage checkout =
            cart.Checkout();

        Assert.That(
            checkout.IsLoaded(),
            Is.True,
            "Checkout information page should be displayed."
        );

        // Step 5: Enter customer information.
        CheckoutOverviewPage overview =
            checkout.EnterCustomerDetails(
                "Peter",
                "Parker",
                "CV1 1AA"
            );

        Assert.That(
            overview.IsLoaded(),
            Is.True,
            "Checkout overview page should be displayed."
        );

        // Step 6: Verify the correct product before purchasing.
        Assert.That(
            overview.ContainsProduct(productName),
            Is.True,
            $"{productName} should still be present in the order overview."
        );

        // Step 7: Finish the order.
        CheckoutCompletePage completePage =
            overview.FinishOrder();

        Assert.That(
            completePage.IsLoaded(),
            Is.True,
            "Checkout completion page should be displayed."
        );

        // Step 8: Verify successful order confirmation.
        Assert.That(
            completePage.GetConfirmationMessage(),
            Is.EqualTo("Thank you for your order!"),
            "Successful checkout should display the order confirmation message."
        );
    }
}