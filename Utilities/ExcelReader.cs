using ClosedXML.Excel;

namespace SauceDemo.Automation.Utilities;

public static class ExcelReader
{
    public static string GetValue(
        string fileName,
        string worksheetName,
        int row,
        int column)
    {
        string filePath = Path.Combine(
            AppContext.BaseDirectory,
            "TestData",
            fileName
        );

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(
                $"Excel test data file was not found: {filePath}"
            );
        }

        using var workbook = new XLWorkbook(filePath);

        if (!workbook.Worksheets.TryGetWorksheet(
                worksheetName,
                out IXLWorksheet? worksheet))
        {
            throw new InvalidOperationException(
                $"Worksheet '{worksheetName}' was not found in {fileName}."
            );
        }

        string value = worksheet.Cell(row, column).GetString();

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException(
                $"No value was found at row {row}, column {column} " +
                $"in worksheet '{worksheetName}'."
            );
        }

        return value;
    }
}