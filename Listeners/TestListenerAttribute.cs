using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SauceDemo.Automation.Utilities;

namespace SauceDemo.Automation.Listeners;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Method,
    AllowMultiple = false
)]
public sealed class TestListenerAttribute : Attribute, ITestAction
{
    public ActionTargets Targets =>
        ActionTargets.Test;

    public void BeforeTest(ITest test)
    {
        LogManager.Logger.Information(
            "Listener - Starting test: {TestName}",
            test.Name
        );
    }

    public void AfterTest(ITest test)
    {
        var result =
            TestContext.CurrentContext.Result;

        if (result.Outcome.Status == TestStatus.Passed)
        {
            LogManager.Logger.Information(
                "Listener - Test passed: {TestName}",
                test.Name
            );
        }
        else if (result.Outcome.Status == TestStatus.Failed)
        {
            LogManager.Logger.Error(
                "Listener - Test failed: {TestName}. {Message}",
                test.Name,
                result.Message
            );
        }
        else if (result.Outcome.Status == TestStatus.Skipped)
        {
            LogManager.Logger.Warning(
                "Listener - Test skipped: {TestName}",
                test.Name
            );
        }
    }
}