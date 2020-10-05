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
        }

        /// <summary>
        /// Shopping cart items list
        /// </summary>
        public List<ShoppingCartArticle> Articles { get; }

        public decimal SalesTaxes { get; set; }
        public decimal Total { get; set; }
    }
}
