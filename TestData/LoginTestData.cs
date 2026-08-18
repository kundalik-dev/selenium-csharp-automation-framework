namespace selenium_csharp_automation_framework.TestData
{
    public class LoginTestData
    {
        public string TestCaseName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool ExpectedSuccess { get; set; }
        public string? ExpectedErrorMessage { get; set; }

        public override string ToString() => TestCaseName;
    }
}
