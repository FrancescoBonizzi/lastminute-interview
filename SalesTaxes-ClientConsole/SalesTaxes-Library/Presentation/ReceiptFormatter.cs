using SalesTaxes_Library.Domain;
using SalesTaxes_Library.Storage;
using System;

namespace SalesTaxes_Library.Presentation
{
    /// <summary>
    /// A service needed to present correctly money amounts and dates
    /// </summary>
    public class ReceiptFormatter
    {
        private readonly ShopConfiguration _shopConfiguration;

        public ReceiptFormatter(IConfigurationRepository configurationRepository)
        {
            _shopConfiguration = configurationRepository.Get();
        }

        /// <summary>
        /// Formats a money amount with the current configured currency
        /// </summary>
        public string FormatMoney(decimal amount)
            => $"{_shopConfiguration.CurrencySymbol} {amount:c2}";

        /// <summary>
        /// Formats a date with the current configured date format
        /// </summary>
        public string FormatDate(DateTime date)
            => date.ToString(_shopConfiguration.DateFormat);
    }
}
