using SalesTaxes_Library.Domain;
using System.Collections.Generic;
using System.Diagnostics;

namespace SalesTaxes_Library.Storage
{
    public class InMemoryShoppingCartRepository : IShoppingCartRepository
    {
        public ShoppingCart New()
        {
            return new ShoppingCart(
                articles: new List<ShoppingCartArticle>(),
                0,
                0);
        }

        public void Save(ShoppingCart shoppingCart)
        {
            Debug.WriteLine("Saved! :-)");
        }
    }
}
