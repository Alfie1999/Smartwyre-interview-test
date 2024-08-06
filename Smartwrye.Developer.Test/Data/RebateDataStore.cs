using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// Implementation of the rebate data store.
    /// This class is responsible for retrieving and storing rebate data.
    /// </summary>
    public class RebateDataStore : IRebateDataStore
    {
        /// <summary>
        /// Retrieves a rebate based on the rebate identifier.
        /// </summary>
        /// <param name="rebateIdentifier">The identifier of the rebate to retrieve.</param>
        /// <returns>The rebate details.</returns>
        public Rebate GetRebate(string rebateIdentifier)
        {
            // Simplified logic for fetching a rebate.
            // In a real-world application, this method would fetch data from a database or another data source.
            return new Rebate { Identifier = rebateIdentifier, Amount = 100, Incentive = IncentiveType.FixedCashAmount };
        }

        /// <summary>
        /// Stores the result of a rebate calculation.
        /// </summary>
        /// <param name="rebate">The rebate details.</param>
        /// <param name="rebateAmount">The calculated rebate amount.</param>
        public void StoreCalculationResult(Rebate rebate, decimal rebateAmount)
        {
            // Simplified logic for storing the result of a rebate calculation.
            // In a real-world application, this method would store the result in a database or another data storage system.
            Console.WriteLine($"Rebate calculation result stored: {rebate.Identifier}, Amount: {rebateAmount}");
        }
    }
}
