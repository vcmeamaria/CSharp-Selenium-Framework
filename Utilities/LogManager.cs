using Serilog;
namespace SauceDemo.Automation.Utilities;
public static class LogManager
{
    private static int _configured;
    public static ILogger Logger { get; private set; } = Serilog.Log.Logger;
    public static void Configure(string directory)
    {
        if (Interlocked.Exchange(ref _configured, 1) == 1) return;
        Directory.CreateDirectory(directory);
        Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.Console().WriteTo.File(Path.Combine(directory,"framework-.log"), rollingInterval: RollingInterval.Day).CreateLogger();
        Serilog.Log.Logger = Logger;
    }
}
