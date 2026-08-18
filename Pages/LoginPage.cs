using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace selenium_csharp_automation_framework.Pages
{
    public class LoginPage : BasePage
    {
        private static readonly By UsernameInput = By.Id("user-name");
        private static readonly By PasswordInput = By.Id("password");
        private static readonly By LoginButton = By.Id("login-button");
        private static readonly By ErrorMessage = By.CssSelector("[data-test='error']");

        public LoginPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public void EnterUsername(string username) => Type(UsernameInput, username);

        public void EnterPassword(string password) => Type(PasswordInput, password);

        public void ClickLogin() => Click(LoginButton);

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public bool IsErrorDisplayed() => IsDisplayed(ErrorMessage);

        public string GetErrorMessage() => GetText(ErrorMessage);
    }
}
