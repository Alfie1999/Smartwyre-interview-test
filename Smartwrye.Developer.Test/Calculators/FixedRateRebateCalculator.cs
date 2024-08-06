using Smartwyre.DeveloperTest.Calculators.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    /// <summary>
    /// Calculator for fixed rate rebate.
    /// Implements the IRebateCalculator interface to calculate the rebate based on a fixed percentage rate.
    /// </summary>
    public class FixedRateRebateCalculator : IFixedRateRebateCalculator
    {
        /// <summary>
        /// Checks if the fixed rate rebate calculator is applicable based on the provided rebate, product, and request.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details.</param>
        /// <param name="request">The rebate calculation request details.</param>
        /// <returns>True if the calculator is applicable; otherwise, false.</returns>
        public bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return request != null && rebate != null && product != null &&
                   product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) &&
                   rebate.Percentage > 0 && product.Price > 0 && request.Volume > 0;
        }

        /// <summary>
        /// Calculates the rebate amount for fixed rate rebate based on the provided rebate, product, and request.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details.</param>
        /// <param name="request">The rebate calculation request details.</param>
        /// <returns>The calculated rebate amount.</returns>
        /// <exception cref="ArgumentNullException">Thrown when rebate, product, or request is null.</exception>
        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            ArgumentNullException.ThrowIfNull(rebate);
            ArgumentNullException.ThrowIfNull(product);
            ArgumentNullException.ThrowIfNull(request);

            return product.Price * rebate.Percentage * request.Volume;
        }
    }
}
