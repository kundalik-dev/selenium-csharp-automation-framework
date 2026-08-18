using selenium_csharp_automation_framework.Base;
using selenium_csharp_automation_framework.Pages;
using selenium_csharp_automation_framework.Utils;

namespace selenium_csharp_automation_framework.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        private static IEnumerable<TestCaseData> LoginTestCases()
        {
            var data = JsonDataProvider.GetTestData<TestData.LoginTestData>(
                Path.Combine("TestData", "loginData.json"));

            foreach (var testCase in data)
            {
                yield return new TestCaseData(testCase).SetName(testCase.TestCaseName);
            }
        }

        [TestCaseSource(nameof(LoginTestCases))]
        public void Login_WithVariousCredentials_BehavesAsExpected(TestData.LoginTestData data)
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Login(data.Username, data.Password);

            if (data.ExpectedSuccess)
            {
                var inventoryPage = new InventoryPage(Driver, Wait);
                Assert.That(inventoryPage.IsDisplayed(), Is.True, "Expected to land on the inventory page after login.");
            }
            else
            {
                Assert.That(loginPage.IsErrorDisplayed(), Is.True, "Expected an error message to be displayed.");
                Assert.That(loginPage.GetErrorMessage(), Does.Contain(data.ExpectedErrorMessage));
            }
        }
    }
}
