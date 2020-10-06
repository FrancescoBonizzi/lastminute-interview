using System;

namespace SalesTaxes_Library.Presentation.Domain
{
    /// <summary>
    /// An article projection ready to be presented
    /// </summary>
    public class ReceiptArticle
    {
        public ReceiptArticle(
            string name,
            string formattedQuantity,
            string formattedPrice,
            string formattedTaxes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(formattedQuantity))
            {
                throw new ArgumentException($"'{nameof(formattedQuantity)}' cannot be null or whitespace", nameof(formattedQuantity));
            }

            if (string.IsNullOrWhiteSpace(formattedPrice))
            {
                throw new ArgumentException($"'{nameof(formattedPrice)}' cannot be null or whitespace", nameof(formattedPrice));
            }

            if (string.IsNullOrWhiteSpace(formattedTaxes))
            {
                throw new ArgumentException($"'{nameof(formattedTaxes)}' cannot be null or whitespace", nameof(formattedTaxes));
            }

            Name = name;
            Quantity = formattedQuantity;
            FormattedPrice = formattedPrice;
            FormattedTaxes = formattedTaxes;
        }

        public string Name { get; }
        public string Quantity { get; }
        public string FormattedPrice { get; }
        public string FormattedTaxes { get; }
    }
}
