using selenium_csharp_automation_framework.Base;
using selenium_csharp_automation_framework.Pages;

namespace selenium_csharp_automation_framework.Tests
{
    // UI test cases from docs/01-test-cases.md, section "2. Inventory Page Test Cases".
    [TestFixture]
    public class InventoryPageUiTests : BaseTest
    {
        private InventoryPage _inventoryPage = null!;

        [SetUp]
        public void LoginAsStandardUser()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Login("standard_user", "secret_sauce");
            _inventoryPage = new InventoryPage(Driver, Wait);
        }

        // INV-UI-01: Should display the correct product information.
        [Test]
        public void ShouldDisplayCorrectProductInformation()
        {
            var backpack = _inventoryPage.GetItemCards().Single(c => c.Name == "Sauce Labs Backpack");

            Assert.Multiple(() =>
            {
                Assert.That(backpack.PriceText, Is.EqualTo("$29.99"));
                Assert.That(backpack.Description, Does.Contain("carry.allTheThings()"));
                Assert.That(backpack.ImageSrc, Is.Not.Empty);
            });
        }

        // INV-UI-02: Should add a single product to the cart and update the badge count to 1.
        [Test]
        public void ShouldAddSingleProductAndUpdateBadgeCountToOne()
        {
            Assert.That(_inventoryPage.IsCartBadgeDisplayed(), Is.False, "Precondition: cart should start empty.");

            var productName = _inventoryPage.GetItemNames().First();
            _inventoryPage.AddProductToCart(productName);

            Assert.That(_inventoryPage.IsCartBadgeDisplayed(), Is.True);
            Assert.That(_inventoryPage.GetCartBadgeCount(), Is.EqualTo(1));
        }

        // INV-UI-03: Should toggle the button text from Add to cart to Remove when a product is added and removed.
        [Test]
        public void ShouldToggleButtonTextWhenProductAddedAndRemoved()
        {
            var productName = _inventoryPage.GetItemNames().First();

            Assert.That(_inventoryPage.GetCartButtonText(productName), Is.EqualTo("Add to cart"));

            _inventoryPage.AddProductToCart(productName);
            Assert.That(_inventoryPage.GetCartButtonText(productName), Is.EqualTo("Remove"));

            _inventoryPage.RemoveProductFromCart(productName);
            Assert.That(_inventoryPage.GetCartButtonText(productName), Is.EqualTo("Add to cart"));
        }

        // INV-UI-04: Should sort product names in descending alphabetical order Z to A.
        [Test]
        public void ShouldSortProductNamesDescendingZToA()
        {
            _inventoryPage.SortBy(InventoryPage.SortNameZToA);

            var names = _inventoryPage.GetItemNames();
            var expectedOrder = names.OrderByDescending(n => n, StringComparer.Ordinal).ToList();

            Assert.That(names, Is.EqualTo(expectedOrder));
        }

        // INV-UI-05: Should sort product names in ascending alphabetical order A to Z.
        [Test]
        public void ShouldSortProductNamesAscendingAToZ()
        {
            _inventoryPage.SortBy(InventoryPage.SortNameAToZ);

            var names = _inventoryPage.GetItemNames();
            var expectedOrder = names.OrderBy(n => n, StringComparer.Ordinal).ToList();

            Assert.That(names, Is.EqualTo(expectedOrder));
        }

        // INV-UI-06: Should sort products by price from low to high.
        [Test]
        public void ShouldSortProductsByPriceLowToHigh()
        {
            _inventoryPage.SortBy(InventoryPage.SortPriceLowToHigh);

            var prices = _inventoryPage.GetItemPrices();
            var expectedOrder = prices.OrderBy(p => p).ToList();

            Assert.That(prices, Is.EqualTo(expectedOrder));
        }

        // INV-UI-07: Should sort products by price from high to low.
        [Test]
        public void ShouldSortProductsByPriceHighToLow()
        {
            _inventoryPage.SortBy(InventoryPage.SortPriceHighToLow);

            var prices = _inventoryPage.GetItemPrices();
            var expectedOrder = prices.OrderByDescending(p => p).ToList();

            Assert.That(prices, Is.EqualTo(expectedOrder));
        }

        // INV-UI-08: Should remove all products from the cart and ensure the badge is hidden.
        [Test]
        public void ShouldRemoveAllProductsAndHideBadge()
        {
            foreach (var name in _inventoryPage.GetItemNames())
                _inventoryPage.AddProductToCart(name);

            Assert.That(_inventoryPage.IsCartBadgeDisplayed(), Is.True, "Precondition: cart should contain items.");

            _inventoryPage.RemoveAllProductsFromCart();

            Assert.That(_inventoryPage.IsCartBadgeDisplayed(), Is.False, "Cart badge should be hidden once the cart is empty.");
        }

        // INV-UI-09: Should display matching images, titles and prices for all inventory cards.
        [Test]
        public void ShouldDisplayMatchingImagesTitlesAndPricesForAllCards()
        {
            var cards = _inventoryPage.GetItemCards();
            Assert.That(cards, Is.Not.Empty);

            Assert.Multiple(() =>
            {
                foreach (var card in cards)
                {
                    Assert.That(card.Name, Is.Not.Empty, "Product name should not be empty.");
                    Assert.That(card.PriceText, Does.StartWith("$"), $"Price for '{card.Name}' should start with '$'.");
                    Assert.That(card.ImageSrc, Is.Not.Empty, $"Image for '{card.Name}' should not be empty.");
                }
            });
        }

        // INV-UI-10: Should retain added cart items after reloading the inventory page.
        [Test]
        public void ShouldRetainCartItemsAfterReload()
        {
            var productName = _inventoryPage.GetItemNames().First();
            _inventoryPage.AddProductToCart(productName);
            Assert.That(_inventoryPage.GetCartBadgeCount(), Is.EqualTo(1));

            Driver.Navigate().Refresh();
            var reloadedPage = new InventoryPage(Driver, Wait);

            Assert.That(reloadedPage.GetCartBadgeCount(), Is.EqualTo(1), "Cart badge count should persist after reload.");
            Assert.That(reloadedPage.GetCartButtonText(productName), Is.EqualTo("Remove"));
        }
    }
}
