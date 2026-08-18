using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace selenium_csharp_automation_framework.Pages
{
    public class LoginPage : BasePage
    {
        private static readonly By Logo = By.ClassName("login_logo");
        private static readonly By UsernameInput = By.Id("user-name");
        private static readonly By PasswordInput = By.Id("password");
        private static readonly By LoginButton = By.Id("login-button");
        private static readonly By ErrorMessage = By.CssSelector("[data-test='error']");
        private static readonly By ErrorCloseButton = By.CssSelector("[data-test='error'] button.error-button");

        public LoginPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public bool IsLogoDisplayed() => IsDisplayed(Logo);

        public bool IsUsernameInputDisplayed() => IsDisplayed(UsernameInput);

        public bool IsPasswordInputDisplayed() => IsDisplayed(PasswordInput);

        public bool IsLoginButtonDisplayed() => IsDisplayed(LoginButton);

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

        public bool IsErrorCloseButtonDisplayed() => IsDisplayed(ErrorCloseButton);

        public void ClickErrorCloseButton() => Click(ErrorCloseButton);

        public string GetUsernameInputBorderColor() => Driver.FindElement(UsernameInput).GetCssValue("border-bottom-color");

        public string GetPasswordInputBorderColor() => Driver.FindElement(PasswordInput).GetCssValue("border-bottom-color");

        public bool HasUsernameInputErrorClass() => Driver.FindElement(UsernameInput).GetAttribute("class")!.Contains("error");

        public bool HasPasswordInputErrorClass() => Driver.FindElement(PasswordInput).GetAttribute("class")!.Contains("error");
    }
}
