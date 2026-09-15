# C# Selenium Framework

A modern automation testing framework built with **C#, Selenium WebDriver and NUnit**.

The framework supports UI testing, API testing, external test data, SQL utilities, logging, screenshots, reporting, parallel execution, Docker, Selenium Grid and CI/CD.

## Run Locally

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

- `SAUCE_BROWSER`
- `SAUCE_HEADLESS`
- `SAUCE_REPORT_TYPE`
- `SAUCE_USERNAME`
- `SAUCE_PASSWORD`
- `SAUCE_API_BASE_URL`
- `SAUCE_DATABASE_CONNECTION_STRING`
- `SELENIUM_GRID_URL`

## Docker and Selenium Grid

The framework can run Selenium tests using a Dockerised Selenium Grid.

The Docker setup contains:

```text
C# Test Container
        |
        v
Selenium Grid Hub
        |
   +----+----+
   |         |
   v         v
Chrome    Firefox
Node       Node
```

### Start Selenium Grid

Make sure Docker Desktop is running.

From the project root, run:

```bash
docker compose up -d
```

Check the running containers:

```bash
docker ps
```

The following containers should be running:

```text
selenium-hub
selenium-chrome
selenium-firefox
```

### Selenium Grid Dashboard

Open:

```text
http://localhost:4444/ui
```

This displays the Selenium Grid dashboard and available Chrome and Firefox nodes.

### Watch Chrome with noVNC

Open:

```text
http://localhost:7900
```

This allows the Chrome browser running inside Docker to be viewed while Selenium tests execute.

### Watch Firefox with noVNC

Open:

```text
http://localhost:7901
```

### Build the C# Test Container

Run:

```bash
docker compose build tests
```

### Run All Demo Tests Through Chrome

```bash
docker compose run --rm tests dotnet test SauceDemo.Automation.csproj --no-restore --filter "FullyQualifiedName~SauceDemoDemoTests" --verbosity normal
```

This runs the five demonstration tests through the Chrome node in Selenium Grid.

### Run a Test Through Firefox

```bash
docker compose run --rm -e SAUCE_BROWSER=firefox tests dotnet test SauceDemo.Automation.csproj --no-restore --filter "FullyQualifiedName~TC_LOGIN_001" --verbosity normal
```

### Stop Selenium Grid

```bash
docker compose down
```

## Reports

Extent reports, logs and screenshots are generated below:

```text
artifacts/
```

Allure results are generated in:

```text
allure-results/
```

### Run Allure Report

After running the tests, navigate to the build output folder:

```bash
cd bin/Debug/net8.0
```

Then open the Allure report:

```bash
allure serve allure-results
```

This generates and opens the interactive Allure HTML report in the browser.

## Demo Test Suite

Five distinct Selenium tests are included for demonstrating different testing approaches.

| Test Case | Type | Scenario |
|---|---|---|
| `TC_LOGIN_001` | Functional / Positive | Login successfully using valid credentials |
| `TC_LOGIN_002` | Functional / Negative | Reject login when an invalid password is used |
| `TC_SECURITY_001` | Security | Block a SQL-injection-style login attempt |
| `TC_SORT_001` | Functional / Usability | Verify products sort correctly from price low to high |
| `TC_E2E_001` | End-to-End / Integration | Complete the full login, cart and checkout journey |

The demo tests can be found in:

```text
Tests/Demo/SauceDemoDemoTests.cs
```

Each test includes its test case ID, type, priority, test data and expected result.

## Project Structure

```text
Config/
├── ConfigurationManager.cs   # Loads configuration and environment variables
└── TestSettings.cs           # Defines framework settings

Core/
├── DriverContext.cs          # Stores WebDriver safely between tests
└── DriverFactory.cs          # Creates local or Selenium Grid browser drivers

Listeners/
└── TestListenerAttribute.cs  # Logs test lifecycle events

Pages/
├── BasePage.cs               # Shared page functionality
├── LoginPage.cs              # Login page actions
├── InventoryPage.cs          # Products and sorting actions
├── CartPage.cs               # Shopping cart actions
├── CheckoutPage.cs           # Customer information step
├── CheckoutOverviewPage.cs   # Order review step
└── CheckoutCompletePage.cs   # Order confirmation step

Reporting/
└── ExtentReportManager.cs    # Generates Extent HTML reports

TestData/
├── loginData.json            # JSON login test data
├── loginData.xlsx            # Excel login test data
└── sample.txt                # File-reader test data

Tests/
├── Demo/
│   └── SauceDemoDemoTests.cs # Five demonstration test cases
├── ApiTests.cs               # API tests
├── BaseTest.cs               # Selenium setup and cleanup
├── FileReaderTests.cs        # File utility tests
├── LoginTests.cs             # Data-driven login tests
└── SqlHelperTests.cs         # SQL helper tests

Utilities/
├── ApiClient.cs              # Sends API requests
├── DatabaseHelper.cs         # Executes database queries
├── ExcelReader.cs            # Reads Excel test data
├── FileReaderHelper.cs       # Reads text files
├── JsonReader.cs             # Reads JSON test data
├── LogManager.cs             # Configures logging
├── ScreenshotUtils.cs        # Captures failure screenshots
├── SqlHelper.cs              # Provides parameterized SQL
└── WaitUtils.cs              # Provides explicit Selenium waits

Properties/
└── AssemblyInfo.cs           # Configures parallel execution

.github/workflows/
└── selenium-tests.yml        # Runs tests with GitHub Actions

Dockerfile                    # Builds the C# Selenium test container
docker-compose.yml            # Creates Selenium Grid and browser nodes
.dockerignore                 # Excludes local build files from Docker
appsettings.json              # Main framework configuration
allureConfig.json             # Allure configuration
SauceDemo.Automation.csproj   # Project dependencies
```

## Features

- Selenium WebDriver
- NUnit
- Page Object Model
- Positive and negative functional testing
- Security test scenario
- Usability validation
- End-to-end checkout testing
- Chrome and Firefox testing
- Selenium Grid
- Docker and Docker Compose
- noVNC browser viewing
- JSON and Excel test data
- RestSharp API testing
- Database and parameterized SQL support
- Serilog logging
- Failure screenshots
- ExtentReports and Allure
- Parallel test execution
- GitHub Actions CI/CD