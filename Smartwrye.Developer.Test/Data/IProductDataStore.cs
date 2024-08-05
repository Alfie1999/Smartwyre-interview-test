using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// Interface for accessing product data.
    /// Provides a method to fetch a product by its identifier.
    /// </summary>
    public interface IProductDataStore
    {
        /// <summary>
        /// Fetches a product by its identifier.
        /// </summary>
        /// <param name="productIdentifier">The identifier of the product to fetch.</param>
        /// <returns>The product details.</returns>
        Product GetProduct(string productIdentifier);
    }
}
