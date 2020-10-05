using SalesTaxes_Library.Domain;

namespace SalesTaxes_Library.Storage
{
    /// <summary>
    /// An abstraction to inject a shopping cart source of data
    /// </summary>
    public interface IShoppingCartRepository
    {
        /// <summary>
        /// Creates a new shopping cart and returns its instance
        /// </summary>
        ShoppingCart New();

        /// <summary>
        /// Persists or updates the shopping cart
        /// </summary>
        void Save(ShoppingCart shoppingCart);
    }
}
