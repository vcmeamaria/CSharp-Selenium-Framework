# C# Selenium Framework

A modern automation testing framework built with **C#, Selenium WebDriver and NUnit**.

The framework supports UI testing, API testing, external test data, SQL utilities, logging, screenshots, reporting, parallel execution and CI/CD.

## Run

Restore the project dependencies:

```bash
dotnet restore
```

Run all tests:

```bash
dotnet test
```

### Configuration Overrides

The framework can be configured using environment variables:

- `SAUCE_BROWSER` - browser to use, such as Chrome
- `SAUCE_HEADLESS` - run the browser without opening a visible window
- `SAUCE_REPORT_TYPE` - reporting option
- `SAUCE_USERNAME` - override the login username
- `SAUCE_PASSWORD` - override the login password
- `SAUCE_API_BASE_URL` - override the API URL
- `SAUCE_DATABASE_CONNECTION_STRING` - database connection string

## Reports

Extent reports, logs and screenshots are generated below:

```text
artifacts/
```

Allure results are generated in:

```text
allure-results/
```

To view an Allure HTML report:

```bash
allure serve allure-results
```

## Project Structure

```text
Config/
├── ConfigurationManager.cs   # Loads configuration and environment variables
└── TestSettings.cs           # Defines framework settings

Core/
├── DriverContext.cs          # Stores the WebDriver safely between tests
└── DriverFactory.cs          # Creates Chrome, Edge or Firefox drivers

Listeners/
└── TestListenerAttribute.cs  # Logs test start, pass, fail and skipped events

Pages/
├── BasePage.cs               # Shared page actions
├── LoginPage.cs              # SauceDemo login page
└── InventoryPage.cs          # SauceDemo inventory page

Reporting/
└── ExtentReportManager.cs    # Creates Extent HTML test reports

TestData/
├── loginData.json            # JSON login test data
├── loginData.xlsx            # Excel login test data
└── sample.txt                # File-reader test data

Tests/
├── ApiTests.cs               # API tests
├── BaseTest.cs               # Selenium setup and cleanup
├── FileReaderTests.cs        # File utility tests
├── LoginTests.cs             # Selenium login tests
└── SqlHelperTests.cs         # SQL helper tests

Utilities/
├── ApiClient.cs              # Sends API requests
├── DatabaseHelper.cs         # Executes database queries
├── ExcelReader.cs            # Reads Excel test data
├── FileReaderHelper.cs       # Reads text files
├── JsonReader.cs             # Reads JSON test data
├── LogManager.cs             # Configures Serilog logging
├── ScreenshotUtils.cs        # Captures failure screenshots
├── SqlHelper.cs              # Provides parameterized SQL queries
└── WaitUtils.cs              # Provides explicit Selenium waits

Properties/
└── AssemblyInfo.cs           # Configures NUnit parallel execution

.github/workflows/
└── selenium-tests.yml        # Runs the test suite with GitHub Actions

appsettings.json              # Main framework configuration
allureConfig.json             # Allure reporting configuration
SauceDemo.Automation.csproj   # Project dependencies and build settings
```

## Features

- Selenium WebDriver
- NUnit
- Page Object Model
- JSON and Excel test data
- RestSharp API testing
- Database and parameterized SQL support
- Serilog logging
- Failure screenshots
- ExtentReports and Allure
- Parallel test execution
- GitHub Actions CI/CD