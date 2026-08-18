using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using selenium_csharp_automation_framework.Config;

namespace selenium_csharp_automation_framework.Base
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected IWebDriver Driver { get; private set; } = null!;
        protected WebDriverWait Wait { get; private set; } = null!;
        protected AppConfig AppSettings { get; private set; } = null!;

        [SetUp]
        public void BaseSetUp()
        {
            AppSettings = ConfigReader.Settings;
            Driver = CreateDriver(AppSettings);

            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(AppSettings.ImplicitWaitSeconds);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(AppSettings.ExplicitWaitSeconds));

            Driver.Navigate().GoToUrl(AppSettings.BaseUrl);
        }

        [TearDown]
        public void BaseTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TryTakeScreenshot();
            }

            Driver.Quit();
            Driver.Dispose();
        }

        private static IWebDriver CreateDriver(AppConfig config)
        {
            return config.Browser.ToLowerInvariant() switch
            {
                "chrome" => CreateChromeDriver(config),
                "firefox" => CreateFirefoxDriver(config),
                _ => throw new NotSupportedException($"Browser '{config.Browser}' is not supported.")
            };
        }

        private static IWebDriver CreateChromeDriver(AppConfig config)
        {
            var options = new ChromeOptions();
            if (config.Headless)
                options.AddArgument("--headless=new");

            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver(AppConfig config)
        {
            var options = new FirefoxOptions();
            if (config.Headless)
                options.AddArgument("-headless");

            return new FirefoxDriver(options);
        }

        private void TryTakeScreenshot()
        {
            try
            {
                var screenshotsDir = Path.Combine(AppContext.BaseDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotsDir);

                var fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
                    .Replace(' ', '_');
                var filePath = Path.Combine(screenshotsDir, fileName);

                ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(filePath);
                TestContext.AddTestAttachment(filePath);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed to capture screenshot: {ex.Message}");
            }
        }
    }
}
