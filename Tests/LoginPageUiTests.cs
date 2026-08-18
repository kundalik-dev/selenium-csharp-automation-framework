using System.Drawing;
using selenium_csharp_automation_framework.Base;
using selenium_csharp_automation_framework.Pages;

namespace selenium_csharp_automation_framework.Tests
{
    // UI test cases from docs/01-test-cases.md, section "1. Login Page Test Cases".
    [TestFixture]
    public class LoginPageUiTests : BaseTest
    {
        // LOGIN-UI-01: Should display all essential login elements when the login page loads.
        [Test]
        public void ShouldDisplayAllEssentialLoginElements()
        {
            var loginPage = new LoginPage(Driver, Wait);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsLogoDisplayed(), Is.True, "Logo should be displayed.");
                Assert.That(loginPage.IsUsernameInputDisplayed(), Is.True, "Username input should be displayed.");
                Assert.That(loginPage.IsPasswordInputDisplayed(), Is.True, "Password input should be displayed.");
                Assert.That(loginPage.IsLoginButtonDisplayed(), Is.True, "Login button should be displayed.");
            });
        }

        // LOGIN-UI-02: Should render responsively on mobile viewports.
        [Test]
        public void ShouldRenderResponsivelyOnMobileViewport()
        {
            Driver.Manage().Window.Size = new Size(390, 844);
            Driver.Navigate().Refresh();

            var loginPage = new LoginPage(Driver, Wait);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsLogoDisplayed(), Is.True, "Logo should be displayed on mobile viewport.");
                Assert.That(loginPage.IsUsernameInputDisplayed(), Is.True, "Username input should be displayed on mobile viewport.");
                Assert.That(loginPage.IsPasswordInputDisplayed(), Is.True, "Password input should be displayed on mobile viewport.");
                Assert.That(loginPage.IsLoginButtonDisplayed(), Is.True, "Login button should be displayed on mobile viewport.");
            });
        }

        // LOGIN-UI-03: Should change the input border color to red when an error state is active.
        [Test]
        public void ShouldChangeInputBorderColorToRedOnErrorState()
        {
            var loginPage = new LoginPage(Driver, Wait);
            var usernameBorderBeforeError = loginPage.GetUsernameInputBorderColor();
            var passwordBorderBeforeError = loginPage.GetPasswordInputBorderColor();

            loginPage.Login("standard_user", "wrong_password");

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.HasUsernameInputErrorClass(), Is.True, "Username input should carry the error class.");
                Assert.That(loginPage.HasPasswordInputErrorClass(), Is.True, "Password input should carry the error class.");
                Assert.That(loginPage.GetUsernameInputBorderColor(), Is.Not.EqualTo(usernameBorderBeforeError));
                Assert.That(loginPage.GetPasswordInputBorderColor(), Is.Not.EqualTo(passwordBorderBeforeError));
                Assert.That(loginPage.GetUsernameInputBorderColor(), Is.EqualTo("rgba(226, 35, 26, 1)"));
                Assert.That(loginPage.GetPasswordInputBorderColor(), Is.EqualTo("rgba(226, 35, 26, 1)"));
            });
        }

        // LOGIN-UI-04: Should display the error container bar at the bottom of the form.
        [Test]
        public void ShouldDisplayErrorContainerBar()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Login("invalid_user", "invalid_password");

            Assert.That(loginPage.IsErrorDisplayed(), Is.True, "Error container should be displayed.");
            Assert.That(loginPage.GetErrorMessage(), Is.Not.Empty, "Error container should contain an error message.");
        }

        // LOGIN-UI-05: Should login with valid credentials.
        [Test]
        public void ShouldLoginWithValidCredentials()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Login("standard_user", "secret_sauce");

            var inventoryPage = new InventoryPage(Driver, Wait);
            Assert.That(inventoryPage.IsDisplayed(), Is.True, "User should be redirected to the inventory page.");
        }

        // LOGIN-UI-06: Should display an error message when a locked-out user's credentials are entered.
        [Test]
        public void ShouldDisplayErrorForLockedOutUser()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Login("locked_out_user", "secret_sauce");

            Assert.That(loginPage.IsErrorDisplayed(), Is.True);
            Assert.That(loginPage.GetErrorMessage(), Does.Contain("Sorry, this user has been locked out."));
        }

        // LOGIN-UI-07: Should display an error message when an invalid username is entered.
        [Test]
        public void ShouldDisplayErrorForInvalidUsername()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Login("invalid_user", "secret_sauce");

            Assert.That(loginPage.IsErrorDisplayed(), Is.True);
            Assert.That(loginPage.GetErrorMessage(), Does.Contain("Username and password do not match any user in this service"));
        }

        // LOGIN-UI-08: Should display an error message when an invalid password is entered.
        [Test]
        public void ShouldDisplayErrorForInvalidPassword()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Login("standard_user", "wrong_password");

            Assert.That(loginPage.IsErrorDisplayed(), Is.True);
            Assert.That(loginPage.GetErrorMessage(), Does.Contain("Username and password do not match any user in this service"));
        }

        // LOGIN-UI-09: Should clear the error message when clicking the close X button.
        [Test]
        public void ShouldClearErrorMessageOnCloseButtonClick()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Login("standard_user", "wrong_password");
            Assert.That(loginPage.IsErrorDisplayed(), Is.True, "Precondition: error should be displayed.");

            loginPage.ClickErrorCloseButton();

            Assert.That(loginPage.IsErrorDisplayed(), Is.False, "Error container should disappear after closing.");
        }
    }
}
