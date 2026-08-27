using NUnit.Framework;
using SauceDemo.Automation.Utilities;

namespace SauceDemo.Automation.Tests;

public sealed class FileReaderTests
{
    [Test]
    public void ReadTextFile_ShouldReturnFileContents()
    {
        string content = FileReaderHelper.ReadTextFile(
            Path.Combine("TestData", "sample.txt")
        );

        Assert.That(
            content.Trim(),
            Is.EqualTo("Selenium framework file reader test.")
        );
    }
}