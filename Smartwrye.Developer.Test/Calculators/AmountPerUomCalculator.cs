using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    /// <summary>
    /// Calculator for Amount Per Unit of Measure (Uom) rebate.
    /// Implements the IRebateCalculator interface to calculate the rebate 
    /// based on a fixed amount per unit of measure of the product sold.
    /// </summary>
    public class AmountPerUomCalculator : IRebateCalculator
    {
        /// <summary>
        /// Determines if the AmountPerUom rebate is applicable based on the provided rebate, product, and request.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details (optional).</param>
        /// <param name="request">The rebate calculation request details (optional).</param>
        /// <returns>True if the calculator is applicable; otherwise, false.</returns>
        public bool IsApplicable(Rebate rebate, Product? product = null, CalculateRebateRequest? request = null)
        {
            ArgumentNullException.ThrowIfNull(rebate);
            ArgumentNullException.ThrowIfNull(product);

            return request != null && product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) &&
                   rebate.Amount > 0 && request.Volume > 0;
        }

        /// <summary>
        /// Calculates the rebate amount for AmountPerUom based on the provided rebate and request.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details (optional).</param>
        /// <param name="request">The rebate calculation request details (optional).</param>
        /// <returns>The total rebate amount based on the amount per unit of measure and the volume.</returns>
        public decimal CalculateRebateAmount(Rebate rebate, Product? product = null, CalculateRebateRequest? request = null)
        {
            ArgumentNullException.ThrowIfNull(rebate);
            ArgumentNullException.ThrowIfNull(request);

            return rebate.Amount * request.Volume;
        }
    }
}
