using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    // Future-Proofing: Including all parameters in the CalculateRebateAmount 
    // method ensures that any future changes or additional logic can be easily 
    // integrated without changing the method signature. 
    // This maintains consistency and flexibility.
    // 
    // Testability: The design makes it easier to unit test each calculator independently,  
    // ensuring that they correctly implement the rebate logic.

    /// <summary>
    /// Interface for calculating rebates.
    /// </summary>
    public interface IRebateCalculator
    {
        /// <summary>
        /// Checks if this calculator is applicable based on the provided rebate, product, and request.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details (optional).</param>
        /// <param name="request">The rebate calculation request details (optional).</param>
        /// <returns>True if the calculator is applicable; otherwise, false.</returns>
        bool IsApplicable(Rebate rebate, Product? product = null, CalculateRebateRequest? request = null);

        /// <summary>
        /// Calculates the rebate amount based on the provided rebate, product, and request.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="product">The product details (optional).</param>
        /// <param name="request">The rebate calculation request details (optional).</param>
        /// <returns>The calculated rebate amount.</returns>
        decimal CalculateRebateAmount(Rebate rebate, Product? product = null, CalculateRebateRequest? request = null);
    }
}
