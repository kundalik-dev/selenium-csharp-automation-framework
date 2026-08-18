using System.Globalization;

namespace selenium_csharp_automation_framework.Utils
{
    /// <summary>
    /// Helpers for parsing and converting saucedemo product price strings (e.g. "$29.99").
    /// </summary>
    public static class PriceUtils
    {
        public static decimal ParsePrice(string priceText)
        {
            if (string.IsNullOrWhiteSpace(priceText))
                throw new ArgumentException("Price text cannot be null or empty.", nameof(priceText));

            var numeric = priceText.Replace("$", string.Empty).Trim();

            if (!decimal.TryParse(numeric, NumberStyles.Number, CultureInfo.InvariantCulture, out var price))
                throw new FormatException($"Unable to parse price from '{priceText}'.");

            return price;
        }

        public static List<decimal> ParsePrices(IEnumerable<string> priceTexts) =>
            priceTexts.Select(ParsePrice).ToList();

        public static decimal SumPrices(IEnumerable<string> priceTexts) =>
            priceTexts.Select(ParsePrice).Sum();

        public static decimal ConvertCurrency(decimal usdAmount, decimal exchangeRate)
        {
            if (exchangeRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(exchangeRate), "Exchange rate must be greater than zero.");

            return Math.Round(usdAmount * exchangeRate, 2);
        }

        public static string FormatAsCurrency(decimal amount, string currencySymbol = "$") =>
            $"{currencySymbol}{amount.ToString("0.00", CultureInfo.InvariantCulture)}";
    }
}
