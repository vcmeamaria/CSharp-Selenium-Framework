namespace SauceDemo.Automation.Utilities;

public static class FileReaderHelper
{
    public static string ReadTextFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException(
                "File path cannot be empty.",
                nameof(filePath)
            );
        }

        string fullPath = Path.IsPathRooted(filePath)
            ? filePath
            : Path.Combine(AppContext.BaseDirectory, filePath);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException(
                $"File was not found: {fullPath}"
            );
        }

        return File.ReadAllText(fullPath);
    }
}