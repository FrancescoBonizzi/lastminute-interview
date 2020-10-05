using System;

namespace SalesTaxes_Library.Domain
{
    /// <summary>
    /// A buyable item
    /// </summary>
    public class Article
    {
        public Article(
            int id,
            string name,
            decimal singleItemPrice,
            ArticleTypes articleType,
            bool isImported)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            SingleItemPrice = singleItemPrice;
            ArticleType = articleType;
            IsImported = isImported;
        }

        /// <summary>
        /// The article unique identifier
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The product name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The single item price.
        /// </summary>
        /// <remarks>I choose <see cref="decimal" /> becaus it is more suitable for financial calculations:
        /// It is a 128-bit data type. 
        /// Compared to floating-point types, has more precision on small numbers</remarks>
        public decimal SingleItemPrice { get; }

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
