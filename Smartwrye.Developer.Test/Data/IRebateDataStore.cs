using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// Interface for accessing rebate data.
    /// Provides methods to fetch a rebate and store the result of a rebate calculation.
    /// </summary>
    public interface IRebateDataStore
    {
        /// <summary>
        /// Fetches a rebate by its identifier.
        /// </summary>
        /// <param name="rebateIdentifier">The identifier of the rebate to fetch.</param>
        /// <returns>The rebate details.</returns>
        Rebate GetRebate(string rebateIdentifier);

        /// <summary>
        /// Stores the result of a rebate calculation.
        /// </summary>
        /// <param name="rebate">The rebate for which the calculation was performed.</param>
        /// <param name="rebateAmount">The calculated rebate amount.</param>
        void StoreCalculationResult(Rebate rebate, decimal rebateAmount);
    }
}
