using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    /// <summary>
    /// Calculator for fixed cash amount rebate.
    /// Implements the IRebateCalculator interface to calculate the rebate as a fixed cash amount.
    /// </summary>
    public class FixedCashAmountCalculator : IRebateCalculator
    {
        /// <summary>
        /// Determines if the FixedCashAmount rebate is applicable based on the provided rebate and product.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details (optional).</param>
        /// <param name="request">The rebate calculation request details (optional).</param>
        /// <returns>True if the calculator is applicable; otherwise, false.</returns>
        public bool IsApplicable(Rebate rebate, Product? product = null, CalculateRebateRequest? request = null)
        {
            return rebate != null && product != null &&
                   product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) &&
                   rebate.Amount > 0;
        }

        /// <summary>
        /// Calculates the rebate amount for FixedCashAmount based on the provided rebate.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details (optional).</param>
        /// <param name="request">The rebate calculation request details (optional).</param>
        /// <returns>The fixed cash rebate amount.</returns>
        /// <remarks>The FixedCashAmount rebate does not depend on product or request details.</remarks>
        public decimal CalculateRebateAmount(Rebate rebate, Product? product = null, CalculateRebateRequest? request = null)
        {
            return rebate.Amount;
        }
    }
}
