using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators.Interfaces
{
    /// <summary>
    /// Interface for rebate calculators that require  Per Unit of Measure  
    /// details in addition to rebate and request details.
    /// </summary>
    public interface IAmountPerUomCalculator 
    {
        /// <summary>
        /// Checks if this calculator is applicable based on the provided rebate, product, and request.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details.</param>
        /// <param name="request">The rebate calculation request details.</param>
        /// <returns>True if the calculator is applicable; otherwise, false.</returns>
        
        bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request);
        /// <summary>
        /// Calculates the rebate amount based on the provided rebate, product, and request.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="request">The rebate calculation request details.</param>
        /// <returns>The calculated rebate amount.</returns>
        decimal CalculateRebateAmount(Rebate rebate, CalculateRebateRequest request);
    }

}
