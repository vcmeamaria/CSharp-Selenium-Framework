using Newtonsoft.Json.Linq;

namespace SauceDemo.Automation.Utilities;

public static class JsonReader
{
    public static string GetValue(string fileName, string section, string key)
    {
        string filePath = Path.Combine(
            AppContext.BaseDirectory,
            "TestData",
            fileName
        );

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(
                $"Test data file was not found: {filePath}"
            );
        }

        string json = File.ReadAllText(filePath);

        JObject data = JObject.Parse(json);

        string? value = data[section]?[key]?.ToString();

        if (string.IsNullOrEmpty(value))
        {
            throw new KeyNotFoundException(
                $"Could not find '{section} -> {key}' in {fileName}."
            );
        }

        return value;
    }
}