using selenium_csharp_automation_framework.Utils;

namespace selenium_csharp_automation_framework.Tests
{
    [TestFixture]
    public class PriceUtilsTests
    {
        [TestCase("$29.99", 29.99)]
        [TestCase("$9.99", 9.99)]
        [TestCase("$0.00", 0.00)]
        public void ParsePrice_ConvertsPriceStringToDecimal(string priceText, decimal expected)
        {
            Assert.That(PriceUtils.ParsePrice(priceText), Is.EqualTo(expected));
        }

        [Test]
        public void ParsePrice_ThrowsOnEmptyString()
        {
            Assert.Throws<ArgumentException>(() => PriceUtils.ParsePrice(""));
        }

        [Test]
        public void SumPrices_ReturnsTotalOfAllPrices()
        {
            var prices = new[] { "$29.99", "$9.99", "$15.99" };

            Assert.That(PriceUtils.SumPrices(prices), Is.EqualTo(55.97m));
        }

        [Test]
        public void ConvertCurrency_AppliesExchangeRateAndRoundsToTwoDecimals()
        {
            var result = PriceUtils.ConvertCurrency(usdAmount: 29.99m, exchangeRate: 0.92m);

            Assert.That(result, Is.EqualTo(27.59m));
        }

        [Test]
        public void ConvertCurrency_ThrowsWhenExchangeRateIsNotPositive()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => PriceUtils.ConvertCurrency(10m, 0m));
        }

        [Test]
        public void FormatAsCurrency_ReturnsSymbolPrefixedTwoDecimalString()
        {
            Assert.That(PriceUtils.FormatAsCurrency(9.5m), Is.EqualTo("$9.50"));
        }
    }
}
