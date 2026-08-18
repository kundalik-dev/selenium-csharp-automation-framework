using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_csharp_automation_framework.Utils;

namespace selenium_csharp_automation_framework.Pages
{
    public class InventoryPage : BasePage
    {
        public const string SortNameAToZ = "az";
        public const string SortNameZToA = "za";
        public const string SortPriceLowToHigh = "lohi";
        public const string SortPriceHighToLow = "hilo";

        private static readonly By InventoryContainer = By.Id("inventory_container");
        private static readonly By ItemCards = By.ClassName("inventory_item");
        private static readonly By ItemPrices = By.ClassName("inventory_item_price");
        private static readonly By ItemNames = By.ClassName("inventory_item_name");
        private static readonly By ItemDescriptions = By.ClassName("inventory_item_desc");
        private static readonly By ItemImages = By.CssSelector(".inventory_item_img img");
        private static readonly By SortDropdown = By.ClassName("product_sort_container");
        private static readonly By CartLink = By.ClassName("shopping_cart_link");
        private static readonly By CartBadge = By.ClassName("shopping_cart_badge");

        public InventoryPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public bool IsDisplayed() => IsDisplayed(InventoryContainer);

        public List<string> GetItemNames() => FindElements(ItemNames).Select(e => e.Text).ToList();

        public List<string> GetItemPriceTexts() => FindElements(ItemPrices).Select(e => e.Text).ToList();

        public List<decimal> GetItemPrices() => PriceUtils.ParsePrices(GetItemPriceTexts());

        public decimal GetTotalPrice() => PriceUtils.SumPrices(GetItemPriceTexts());

        public List<InventoryItemCard> GetItemCards()
        {
            return FindElements(ItemCards).Select(card => new InventoryItemCard
            {
                Name = card.FindElement(ItemNames).Text,
                Description = card.FindElement(ItemDescriptions).Text,
                PriceText = card.FindElement(ItemPrices).Text,
                ImageSrc = card.FindElement(ItemImages).GetAttribute("src") ?? string.Empty
            }).ToList();
        }

        public void SortBy(string sortOptionValue)
        {
            var dropdown = new SelectElement(FindElement(SortDropdown));
            dropdown.SelectByValue(sortOptionValue);
        }

        public bool IsCartBadgeDisplayed() => IsDisplayed(CartBadge);

        public int GetCartBadgeCount() => IsCartBadgeDisplayed() ? int.Parse(GetText(CartBadge)) : 0;

        public void ClickCartLink() => Click(CartLink);

        public void AddProductToCart(string productName) => Click(AddOrRemoveButtonFor(productName));

        public void RemoveProductFromCart(string productName) => Click(AddOrRemoveButtonFor(productName));

        public string GetCartButtonText(string productName) => FindElement(AddOrRemoveButtonFor(productName)).Text;

        public void RemoveAllProductsFromCart()
        {
            foreach (var name in GetItemNames())
            {
                if (GetCartButtonText(name).Equals("Remove", StringComparison.OrdinalIgnoreCase))
                    RemoveProductFromCart(name);
            }
        }

        private static By AddOrRemoveButtonFor(string productName) => By.XPath(
            $"//div[contains(@class,'inventory_item')][.//div[contains(@class,'inventory_item_name')][text()='{productName}']]//button");
    }
}
