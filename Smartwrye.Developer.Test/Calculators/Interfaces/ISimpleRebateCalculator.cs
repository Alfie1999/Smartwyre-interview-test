using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators.Interfaces
{
    /// <summary>
    /// Interface for simple rebate calculators that only require rebate details.
    /// </summary>
    public interface ISimpleRebateCalculator 
    {
        /// <summary>
        /// Checks if this calculator is applicable based on the provided rebate.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <returns>True if the calculator is applicable; otherwise, false.</returns>
        bool IsApplicable(Rebate rebate);

        /// <summary>
        /// Calculates the rebate amount based on the provided rebate.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <returns>The calculated rebate amount.</returns>
        decimal CalculateRebateAmount(Rebate rebate);
    }

}
