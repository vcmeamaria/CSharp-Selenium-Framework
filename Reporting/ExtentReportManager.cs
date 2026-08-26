using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
namespace SauceDemo.Automation.Reporting;
public static class ExtentReportManager
{
    private static readonly object Sync = new();
    private static ExtentReports? _extent;
    private static readonly AsyncLocal<ExtentTest?> Current = new();
    public static bool Enabled { get; private set; }
    public static void Initialise(string reportType, string directory)
    {
        Enabled = reportType.Equals("extent", StringComparison.OrdinalIgnoreCase) || reportType.Equals("both", StringComparison.OrdinalIgnoreCase);
        if (!Enabled || _extent is not null) return;
        lock(Sync)
        {
            if (_extent is not null) return;
            Directory.CreateDirectory(directory);
            var reporter = new ExtentSparkReporter(Path.Combine(directory,$"ExtentReport_{DateTime.Now:yyyyMMdd_HHmmss}.html"));
            _extent = new ExtentReports(); _extent.AttachReporter(reporter); _extent.AddSystemInfo("Framework","Selenium C#");
        }
    }
    public static void Start(string name) { if(Enabled) Current.Value = _extent!.CreateTest(name); }
    public static void Pass(string message) { if(Enabled) Current.Value?.Pass(message); }
    public static void Fail(string message, string? screenshot=null) { if(!Enabled)return; Current.Value?.Fail(message); if(screenshot is not null) Current.Value?.AddScreenCaptureFromPath(screenshot); }
    public static void Flush() { if(Enabled) lock(Sync) _extent?.Flush(); }
}
