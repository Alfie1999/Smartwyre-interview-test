namespace Smartwyre.DeveloperTest.Types
{
    /// <summary>
    /// Represents the types of incentives that a product can support.
    /// Uses bitwise flags to allow combining multiple incentive types.
    /// </summary>
    [Flags]
    public enum SupportedIncentiveType
    {
        /// <summary>
        /// Represents a fixed rate rebate incentive type.
        /// </summary>
        FixedRateRebate = 1 << 0, // 1 (binary 0001)

        /// <summary>
        /// Represents an amount per unit of measure (Uom) incentive type.
        /// </summary>
        AmountPerUom = 1 << 1,    // 2 (binary 0010)

        /// <summary>
        /// Represents a fixed cash amount incentive type.
        /// </summary>
        FixedCashAmount = 1 << 2, // 4 (binary 0100)
    }
}
