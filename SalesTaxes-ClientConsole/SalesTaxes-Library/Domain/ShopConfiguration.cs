using SalesTaxes_Library.Presentation.Domain;
using System;
using System.Collections.Generic;

namespace SalesTaxes_Library.Domain
{
    /// <summary>
    /// The shop configuration with definitions for taxes and formatting
    /// </summary>
    public class ShopConfiguration
    {
        public ShopConfiguration(
            decimal importDutyTaxPercentage,
            Dictionary<ArticleTypes, decimal> basicSalesTax,
            string currencySymbol,
            string dateFormat,
            string htmlReceiptTemplateFoldersPath,
            ReceiptBillingCompany receiptBillingCompany)
        {
            if (string.IsNullOrWhiteSpace(currencySymbol))
            {
                throw new ArgumentException(
                    $"'{nameof(currencySymbol)}' cannot be null or whitespace",
                    nameof(currencySymbol));
            }

            if (string.IsNullOrWhiteSpace(dateFormat))
            {
                throw new ArgumentException(
                    $"'{nameof(dateFormat)}' cannot be null or whitespace",
                    nameof(dateFormat));
            }

            if (string.IsNullOrWhiteSpace(htmlReceiptTemplateFoldersPath))
            {
                throw new ArgumentException(
                    $"'{nameof(htmlReceiptTemplateFoldersPath)}' cannot be null or whitespace",
                    nameof(htmlReceiptTemplateFoldersPath));
            }

            ImportDutyTaxPercentage = importDutyTaxPercentage;
            BasicSalesTax = basicSalesTax ?? throw new ArgumentNullException(nameof(basicSalesTax));
            CurrencySymbol = currencySymbol;
            DateFormat = dateFormat;
            HtmlReceiptTemplateFoldersPath = htmlReceiptTemplateFoldersPath;
            ReceiptBillingCompany = receiptBillingCompany ?? throw new ArgumentNullException(nameof(receiptBillingCompany));
        }

        /// <summary>
        /// The tax applicable on all imported goods
        /// </summary>
        public decimal ImportDutyTaxPercentage { get; }

        /// <summary>
        /// The tax applied to an article receipt based on an <see cref="ArticleTypes"/>
        /// </summary>
        public Dictionary<ArticleTypes, decimal> BasicSalesTax { get; }

        /// <summary>
        /// The current shop currency symbol (€, £, $...)
        /// </summary>
        public string CurrencySymbol { get; }

        /// <summary>
        /// The current shop date format
        /// </summary>
        public string DateFormat { get; }

        /// <summary>
        /// A folder that should contain html assets to render a receipt
        /// </summary>
        public string HtmlReceiptTemplateFoldersPath { get; }

        /// <summary>
        /// The company details of this shop, to be used in the final receipt
        /// </summary>
        public ReceiptBillingCompany ReceiptBillingCompany { get; }
    }
}
