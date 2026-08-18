using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace selenium_csharp_automation_framework.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        protected BasePage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public void NavigateTo(string url) => Driver.Navigate().GoToUrl(url);

        protected IWebElement FindElement(By locator) =>
            Wait.Until(ExpectedConditions.ElementExists(locator));

        protected IReadOnlyCollection<IWebElement> FindElements(By locator) =>
            Wait.Until(drv =>
            {
                var elements = drv.FindElements(locator);
                return elements.Count > 0 ? elements : null;
            })!;

        protected IWebElement WaitForClickable(By locator) =>
            Wait.Until(ExpectedConditions.ElementToBeClickable(locator));

        protected void Click(By locator) => WaitForClickable(locator).Click();

        protected void Type(By locator, string text)
        {
            var element = FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By locator) => FindElement(locator).Text;

        protected bool IsDisplayed(By locator)
        {
            try
            {
                return Driver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
