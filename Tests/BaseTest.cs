using Allure.Net.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SauceDemo.Automation.Config;
using SauceDemo.Automation.Core;
using SauceDemo.Automation.Reporting;
using SauceDemo.Automation.Utilities;
namespace SauceDemo.Automation.Tests;
public abstract class BaseTest
{
    protected TestSettings Settings => ConfigurationManager.Settings;
    [OneTimeSetUp] public void GlobalSetup(){ LogManager.Configure(Settings.LogDirectory); ExtentReportManager.Initialise(Settings.ReportType,Settings.ExtentReportDirectory); }
    [SetUp] public void Setup(){ DriverContext.Set(DriverFactory.Create(Settings)); ExtentReportManager.Start(TestContext.CurrentContext.Test.Name); LogManager.Logger.Information("Starting {Test}",TestContext.CurrentContext.Test.Name); }
    [TearDown] public void TearDown()
    {
        var result=TestContext.CurrentContext.Result;
        try
        {
            if(result.Outcome.Status==TestStatus.Failed)
            {
                var shot=ScreenshotUtils.Capture(DriverContext.Driver,Settings.ScreenshotDirectory,TestContext.CurrentContext.Test.Name);
                TestContext.AddTestAttachment(shot,"Failure screenshot");
                if(Settings.ReportType.Equals("allure",StringComparison.OrdinalIgnoreCase)||Settings.ReportType.Equals("both",StringComparison.OrdinalIgnoreCase)) AllureApi.AddAttachment("Failure screenshot","image/png",File.ReadAllBytes(shot),"png");
                ExtentReportManager.Fail(result.Message ?? "Test failed",shot);
                LogManager.Logger.Error("Test failed: {Message}",result.Message);
            }
            else { ExtentReportManager.Pass("Test passed"); LogManager.Logger.Information("Test passed"); }
        }
        finally { DriverContext.Quit(); ExtentReportManager.Flush(); }
    }
}
