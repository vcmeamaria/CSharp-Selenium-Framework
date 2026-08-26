# C# Selenium Framework

## Run
```bash
dotnet restore
dotnet test
```
Overrides: `SAUCE_BROWSER`, `SAUCE_HEADLESS`, `SAUCE_REPORT_TYPE`, `SAUCE_USERNAME`, `SAUCE_PASSWORD`.
Reports are written below `artifacts/`. Generate Allure HTML with `allure serve artifacts/allure-results`.
