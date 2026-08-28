using System.Diagnostics;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace SauceDemo.Automation.Reporting;

public static class ExtentReportManager
{
    private static readonly object Sync = new();

    private static ExtentReports? _extent;

    private static readonly AsyncLocal<ExtentTest?> Current = new();

    private static readonly AsyncLocal<Stopwatch?> CurrentTimer = new();

    public static bool Enabled { get; private set; }

    public static void Initialise(
        string reportType,
        string directory,
        string logDirectory)
    {
        Enabled =
            reportType.Equals(
                "extent",
                StringComparison.OrdinalIgnoreCase
            )
            ||
            reportType.Equals(
                "both",
                StringComparison.OrdinalIgnoreCase
            );

        if (!Enabled || _extent is not null)
        {
            return;
        }

        lock (Sync)
        {
            if (_extent is not null)
            {
                return;
            }

            string reportDirectory = Path.IsPathRooted(directory)
                ? directory
                : Path.Combine(
                    AppContext.BaseDirectory,
                    directory
                );

            string fullLogDirectory = Path.IsPathRooted(logDirectory)
                ? logDirectory
                : Path.Combine(
                    AppContext.BaseDirectory,
                    logDirectory
                );

            Directory.CreateDirectory(reportDirectory);

            string reportPath = Path.Combine(
                reportDirectory,
                $"ExtentReport_{DateTime.Now:yyyyMMdd_HHmmss}.html"
            );

            var reporter =
                new ExtentSparkReporter(reportPath);

            _extent = new ExtentReports();

            _extent.AttachReporter(reporter);

            _extent.AddSystemInfo(
                "Framework",
                "Selenium C#"
            );

            _extent.AddSystemInfo(
                "Runtime",
                ".NET 8"
            );

            _extent.AddSystemInfo(
                "Test Framework",
                "NUnit"
            );

            _extent.AddSystemInfo(
                "Logs",
                fullLogDirectory
            );
        }
    }

    public static void Start(string name)
    {
        if (!Enabled)
        {
            return;
        }

        Current.Value =
            _extent!.CreateTest(name);

        CurrentTimer.Value =
            Stopwatch.StartNew();

        Current.Value.Info(
            $"Test started at {DateTime.Now:HH:mm:ss}"
        );
    }

    public static void Pass(string message)
    {
        if (!Enabled)
        {
            return;
        }

        TimeSpan duration =
            StopTimer();

        Current.Value?.Pass(message);

        Current.Value?.Info(
            $"Duration: {duration.TotalSeconds:F2} seconds"
        );
    }

    public static void Fail(
        string message,
        string? screenshot = null,
        string? stackTrace = null)
    {
        if (!Enabled)
        {
            return;
        }

        TimeSpan duration =
            StopTimer();

        Current.Value?.Fail(message);

        Current.Value?.Info(
            $"Duration: {duration.TotalSeconds:F2} seconds"
        );

        if (!string.IsNullOrWhiteSpace(stackTrace))
        {
            Current.Value?.Info(
                $"Stack trace:{Environment.NewLine}{stackTrace}"
            );
        }

        if (!string.IsNullOrWhiteSpace(screenshot))
        {
            Current.Value?.AddScreenCaptureFromPath(
                screenshot
            );
        }
    }

    public static void Flush()
    {
        if (!Enabled)
        {
            return;
        }

        lock (Sync)
        {
            _extent?.Flush();
        }
    }

    private static TimeSpan StopTimer()
    {
        Stopwatch? timer =
            CurrentTimer.Value;

        if (timer is null)
        {
            return TimeSpan.Zero;
        }

        timer.Stop();

        return timer.Elapsed;
    }
}