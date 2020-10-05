using SalesTaxes_Library.Domain;

namespace SalesTaxes_Library.Storage
{
    /// <summary>
    /// The shop configuration provider
    /// </summary>
    public interface IConfigurationRepository
    {
        /// <summary>
        /// Returns the current shop configuration
        /// </summary>
        ShopConfiguration Get();
    }
}
