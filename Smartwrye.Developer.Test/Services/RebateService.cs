using Smartwyre.DeveloperTest.Calculators.Interfaces;
using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Smartwrye.Developer.Test.Types; // Potential typo: verify the correctness of this namespace
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    /// <summary>
    /// Defines the contract for a rebate service.
    /// </summary>
    public interface IRebateService
    {
        // This interface is currently empty but is intended to define methods for rebate services.
    }

    /// <summary>
    /// Provides implementation for calculating rebates.
    /// </summary>
    public class RebateService : IRebateService
    {
        // Private fields to hold the data stores and rebate calculators.
        private readonly IRebateDataStore _rebateDataStore;
        private readonly IProductDataStore _productDataStore;
        private readonly IEnumerable<IRebateCalculator> _rebateCalculators;

        /// <summary>
        /// Initializes a new instance of the <see cref="RebateService"/> class.
        /// </summary>
        /// <param name="rebateDataStore">The data store used to retrieve and store rebate information.</param>
        /// <param name="productDataStore">The data store used to retrieve product information.</param>
        /// <param name="rebateCalculators">The collection of rebate calculators to determine applicable rebates.</param>
        public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore,
            IEnumerable<IRebateCalculator> rebateCalculators)
        {
            _rebateDataStore = rebateDataStore;
            _productDataStore = productDataStore;
            _rebateCalculators = rebateCalculators;
        }

        /// <summary>
        /// Calculates the rebate based on the provided request.
        /// </summary>
        /// <param name="request">The request containing rebate and product identifiers for calculation.</param>
        /// <returns>A result indicating whether the rebate calculation was successful or not.</returns>
        public CalculateRebateResult Calculate(CalculateRebateRequest request)
        {
            // Retrieve rebate and product information using the provided identifiers.
            var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
            var product = _productDataStore.GetProduct(request.ProductIdentifier);

            // Iterate through all rebate calculators to find the one that is applicable.
            foreach (var calculator in _rebateCalculators)
            {
                // Check if the current calculator can handle the given rebate and product.
                if (calculator.IsApplicable(rebate, product, request))
                {
                    // Calculate the rebate amount using the applicable calculator.
                    var rebateAmount = calculator.CalculateRebateAmount(rebate, product, request);

                    // Store the calculated rebate amount in the data store.
                    _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);

                    // Return a result indicating the calculation was successful.
                    return new CalculateRebateResult { Success = true };
                }
            }

            // Return a result indicating the calculation was unsuccessful if no calculator was found.
            return new CalculateRebateResult { Success = false };
        }
    }
}
