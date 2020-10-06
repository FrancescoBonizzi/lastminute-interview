using System;
using System.Collections.Generic;

namespace SalesTaxes_Library.Domain
{
    public class ShoppingCart
    {
        public ShoppingCart(
            List<ShoppingCartArticle> articles,
            decimal salesTaxes,
            decimal total)
        {
            Articles = articles ?? throw new ArgumentNullException(nameof(articles));
            SalesTaxes = salesTaxes;
            Total = total;

            CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Shopping cart items list
        /// </summary>
        public List<ShoppingCartArticle> Articles { get; }

        /// <summary>
        /// The amount of taxes applied to this cart
        /// </summary>
        public decimal SalesTaxes { get; set; }

        /// <summary>
        /// The total price of this cart
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// This cart creation date
        /// </summary>
        public DateTime CreationDate { get; }
    }
}
