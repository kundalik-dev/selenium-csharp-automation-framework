using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_csharp_automation_framework.Utils;

namespace selenium_csharp_automation_framework.Pages
{
    public class InventoryPage : BasePage
    {
        private static readonly By InventoryContainer = By.Id("inventory_container");
        private static readonly By ItemPrices = By.ClassName("inventory_item_price");
        private static readonly By ItemNames = By.ClassName("inventory_item_name");

        public InventoryPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public bool IsDisplayed() => IsDisplayed(InventoryContainer);

        public List<string> GetItemNames() => FindElements(ItemNames).Select(e => e.Text).ToList();

        public List<string> GetItemPriceTexts() => FindElements(ItemPrices).Select(e => e.Text).ToList();

        public List<decimal> GetItemPrices() => PriceUtils.ParsePrices(GetItemPriceTexts());

        public decimal GetTotalPrice() => PriceUtils.SumPrices(GetItemPriceTexts());
    }
}
