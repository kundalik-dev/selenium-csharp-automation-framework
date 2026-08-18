using selenium_csharp_automation_framework.Base;
using selenium_csharp_automation_framework.Pages;
using selenium_csharp_automation_framework.Utils;

namespace selenium_csharp_automation_framework.Tests
{
    [TestFixture]
    public class InventoryPriceTests : BaseTest
    {
        [Test]
        public void GetTotalPrice_AfterLogin_MatchesSumOfIndividualItemPrices()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Login("standard_user", "secret_sauce");

            var inventoryPage = new InventoryPage(Driver, Wait);
            Assert.That(inventoryPage.IsDisplayed(), Is.True);

            var priceTexts = inventoryPage.GetItemPriceTexts();
            Assert.That(priceTexts, Is.Not.Empty);

            var expectedTotal = priceTexts.Sum(PriceUtils.ParsePrice);
            var actualTotal = inventoryPage.GetTotalPrice();

            Assert.That(actualTotal, Is.EqualTo(expectedTotal));
        }
    }
}
