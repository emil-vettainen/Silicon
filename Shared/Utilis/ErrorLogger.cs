using System.Diagnostics;

namespace Shared.Utilis;

public class ErrorLogger(string filePath)
{
    private readonly string _filePath = filePath;

    public void ErrorLog(string message, string method)
    {
        try
        {
            var logMessage = $"{DateTime.Now}, method:{method} :: {message}";
            Debug.WriteLine(logMessage);

            using var sw = new StreamWriter(_filePath, true);
            sw.WriteLine(message);
        }
        catch (Exception ex) 
        { 
            Debug.WriteLine($"LOG ERROR! {DateTime.Now}, method:{nameof(ErrorLog)} :: {ex.Message}");
        }
    }
}
