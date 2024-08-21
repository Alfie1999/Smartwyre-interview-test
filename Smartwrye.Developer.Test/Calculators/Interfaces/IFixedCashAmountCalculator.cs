using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators.Interfaces
{
    /// <summary>
    /// Interface for fixed cash amount calculators.
    /// </summary>
    public interface IFixedCashAmountCalculator 
    {
        /// <summary>
        /// Checks if this calculator is applicable based on the provided rebate and product.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details.</param>
        /// <returns>True if the calculator is applicable; otherwise, false.</returns>
        bool IsApplicable(Rebate rebate, Product product);

        /// <summary>
        /// Calculates the rebate amount based on the provided rebate and product.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details.</param>
        /// <returns>The fixed cash rebate amount.</returns>
        decimal CalculateRebateAmount(Rebate rebate, Product product);


    }
}
