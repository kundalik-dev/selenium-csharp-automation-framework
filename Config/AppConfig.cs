namespace selenium_csharp_automation_framework.Config
{
    public class AppConfig
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string Browser { get; set; } = "chrome";
        public bool Headless { get; set; } = false;
        public int ImplicitWaitSeconds { get; set; } = 5;
        public int ExplicitWaitSeconds { get; set; } = 10;
    }
}
