using System.Text.Json;

namespace selenium_csharp_automation_framework.Utils
{
    /// <summary>
    /// Reads JSON test-data files from the TestData folder for data-driven tests.
    /// </summary>
    public static class JsonDataProvider
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static List<T> GetTestData<T>(string relativePath)
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Test data file not found: '{fullPath}'.");

            var json = File.ReadAllText(fullPath);
            return JsonSerializer.Deserialize<List<T>>(json, Options)
                   ?? new List<T>();
        }
    }
}
