using System;

namespace SalesTaxes_Library.Domain
{
    /// <summary>
    /// An item added to a shopping cart
    /// </summary>
    public class ShoppingCartArticle
    {
        /// <summary>
        /// The constructor: I don't want that the object could be malformed or be modified
        /// </summary>
        public ShoppingCartArticle(
            int articleId,
            string name,
            int quantity,
            decimal singleItemPrice,
            decimal totalTaxes,
            ArticleTypes articleType,
            bool isImported)
        {
            ArticleId = articleId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity;
            SingleItemPrice = singleItemPrice;
            TotalTaxes = totalTaxes;
            ArticleType = articleType;
            IsImported = isImported;
        }

        /// <summary>
        /// The original article unique identifier
        /// </summary>
        public int ArticleId { get; }

        /// <summary>
        /// The product name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The product quantity, int because I don't imagine a fraction of a book
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        /// The single item price.
        /// </summary>
        /// <remarks>I choose <see cref="decimal" /> becaus it is more suitable for financial calculations:
        /// It is a 128-bit data type. 
        /// Compared to floating-point types, has more precision and a smaller range</remarks>
        public decimal SingleItemPrice { get; }

        /// <summary>
        /// The total taxes of this cart item
        /// </summary>
        public decimal TotalTaxes { get; }

        /// <summary>
        /// The total price with calculated taxes
        /// </summary>
        public decimal TotalPrice => SingleItemPrice * Quantity + TotalTaxes;

        /// <summary>
        /// The kind of item
        /// </summary>
        public ArticleTypes ArticleType { get; }

        /// <summary>
        /// Specifies if this is an imported good
        /// </summary>
        public bool IsImported { get; }
    }
}
