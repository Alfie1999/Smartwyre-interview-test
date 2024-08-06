using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// Implementation of the product data store.
    /// This class is responsible for retrieving product data.
    /// </summary>
    public class ProductDataStore : IProductDataStore
    {
        /// <summary>
        /// Retrieves a product based on the product identifier.
        /// </summary>
        /// <param name="productIdentifier">The identifier of the product to retrieve.</param>
        /// <returns>The product details.</returns>
        public Product GetProduct(string productIdentifier)
        {
            // Simplified logic for fetching a product.
            // In a real-world application, this method would fetch data from a database or another data source.
            return new Product { Identifier = productIdentifier, Price = 50, SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        }
    }
}
