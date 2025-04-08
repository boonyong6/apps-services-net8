using Microsoft.Data.SqlClient; // To use SqlConnection.
using System.Collections; // To use IDictionary.
using System.Globalization; // To use CultureInfo.

partial class Program
{
    private static void ConfigureConsole(string culture = "en-US", 
        bool useComputerCulture = false)
    {
        OutputEncoding = System.Text.Encoding.UTF8;
        if (!useComputerCulture)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
        }
        WriteLine($"CurrentCulture: {CultureInfo.CurrentCulture.DisplayName}");
    }

    private static void WriteLineInColor(string value, 
        ConsoleColor color = ConsoleColor.White)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = color;
        WriteLine(value);
        ForegroundColor = previousColor;
    }

    private static void OutputStatistics(SqlConnection connection)
    {
        string[] includeKeys = [
            "BytesSent", "BytesReceived", "ConnectionTime", "SelectRows"];

        IDictionary statistics = connection.RetrieveStatistics();
        foreach (object? key in statistics.Keys)
        {
            bool isIncludeKey = !includeKeys.Any() || includeKeys.Contains(key);
            if (!isIncludeKey)
            {
                continue;
            }

            if (int.TryParse(statistics[key]?.ToString(), out int value))
            {
                WriteLineInColor($"{key}: {value:N0}", ConsoleColor.Cyan);
            }
        }
    }
}
